using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoWD.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Data.Entity;

namespace LoWD.Controllers
{
    public class HomeController : Controller
    {
        private LoWDEntities db = new LoWDEntities();

        public class allGames
        {
            public int game_id { get; set; }
            public bool undermountain_flag { get; set; }
            public bool skullport_flag { get; set; }
            public bool plus_one_flag { get; set; }
            public System.DateTime create_date { get; set; }
            public int user_id { get; set; }
            public int lord_id { get; set; }
            public int lord_pts { get; set; }
            public int corruption_pts { get; set; }
            public int gold_pts { get; set; }
            public int adv_pts { get; set; }
            public int quest_pts { get; set; }
            public int quest_qty { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string user_name { get; set; }
            public string fname { get; set; }
            public string lname { get; set; }
        }
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult newGame(string users, string gameInfo)
        {
            JObject jo = JObject.Parse(gameInfo);
            jo["plus_one_flag"] = Convert.ToBoolean(Convert.ToInt32(jo["plus_one_flag"]));
            jo["undermountain_flag"] = Convert.ToBoolean(Convert.ToInt32(jo["undermountain_flag"]));
            jo["skullport_flag"] = Convert.ToBoolean(Convert.ToInt32(jo["skullport_flag"]));
            var gameInfoObject = jo.ToObject<game>();

            DateTime gametime = new DateTime(2014,10,16);

            string sqlStr = "Insert into game (undermountain_flag, skullport_flag, plus_one_flag, create_date) Values ({0}, {1}, {2}, {3})";
            sqlStr += " Select Convert(int, Scope_Identity()) AS NewID";

            var id = db.Database.SqlQuery<int>(sqlStr, gameInfoObject.undermountain_flag, gameInfoObject.skullport_flag, gameInfoObject.plus_one_flag, gametime).Single();
  

            JArray objects = JArray.Parse(users);
            foreach (JObject item in objects)
            {
                var that = item.ToObject<game_played>();

                string sqlStr1 = "Insert into game_played (game_id, user_id, lord_id, lord_pts, corruption_pts, gold_pts, adv_pts, quest_pts, quest_qty)";
                sqlStr1 += " Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})";                
                db.Database.ExecuteSqlCommand(sqlStr1, id, that.user_id, that.lord_id, that.lord_pts, that.corruption_pts, that.gold_pts, that.adv_pts, that.quest_pts, that.quest_qty);
                
            }
            Response.Write(id);

            return new EmptyResult();
        }

        public ActionResult getInfo()
        {
            string sqlStr = "Select lord_id, name, description From lord order by name";            
            string sqlStr1 = "Select user_id, user_name, fname, lname From [user] order by fname";

            var Lords = db.Database.SqlQuery<lord>(sqlStr).ToList();
            var Users = db.Database.SqlQuery<user>(sqlStr1).ToList();

            string newJSON = JsonConvert.SerializeObject(new { lords = Lords, users = Users });
            Response.ContentType = "application/json";
            Response.Write(newJSON);

            return new EmptyResult();
        }

        public ActionResult getGames()
        {
            string sqlStr = @"Select a.undermountain_flag, a.skullport_flag, a.plus_one_flag, a.create_date, b.game_id, b.user_id, b.lord_id, b.lord_pts, b.corruption_pts,
                b.gold_pts, b.adv_pts, b.quest_pts, b.quest_qty, c.name, c.description, d.user_name, d.fname, d.lname
                From game a inner join
                game_played b on a.game_id = b.game_id inner join
                lord c on c.lord_id = b.lord_id inner join
                [user] d on b.user_id = d.user_id";

            var data = db.Database.SqlQuery<allGames>(sqlStr).ToList();

            string json = JsonConvert.SerializeObject(data);
            Response.ContentType = "application/json";
            Response.Write(json);

            return new EmptyResult();
        }

        public ActionResult getLeaderboard()
        {
            string sqlStr = @"
                Select a.player_qty, a.plus_one_flag, a.skullport_flag, a.undermountain_flag,
	                (
		                Select u.user_name, SUM(gp.lord_pts + gp.gold_pts + gp.adv_pts - gp.corruption_pts + gp.quest_pts) TotalPoints, Count(gp.game_id) GamesPLayed,
		                SUM(gp.lord_pts + gp.gold_pts + gp.adv_pts - gp.corruption_pts + gp.quest_pts) / Count(gp.game_id) AvgPts, SUM(drv.Points) PlacePts,
		                SUM(drv.Points) / CONVERT(decimal(4,2), Count(gp.game_id)) AvgPlacePts
		                From game_played gp inner join
			                [user] u on gp.user_id = u.user_id inner join
			                (
				                Select gp.game_id, gp.user_id, RANK() OVER (partition by gp.game_id ORDER BY SUM(gp.lord_pts + gp.gold_pts + gp.adv_pts - gp.corruption_pts + gp.quest_pts) desc) Points, u.user_name
				                From game_played gp inner join
					                [user] u on gp.user_id = u.user_id
				                Group By gp.user_id, u.user_name, gp.game_id
			                ) drv on gp.game_id = drv.game_id AND gp.user_id = drv.user_id
		                Where gp.game_id in 
		                (
			                Select z.game_id
			                From game z inner join
				                game_played b on z.game_id = b.game_id
			                Group by z.game_id, z.skullport_flag, z.undermountain_flag, z.plus_one_flag
			                Having Count(b.user_id) = a.player_qty
			                AND z.skullport_flag = a.skullport_flag
			                AND z.undermountain_flag = a.undermountain_flag
			                AND z.plus_one_flag  = a.plus_one_flag
		                )
		                Group By u.user_name
		                Order By SUM(drv.Points) / CONVERT(decimal(4,2), Count(gp.game_id)), Count(gp.game_id) desc
		
		                For XML Path('leader'), type
			                )
			
                From (
	                Select Count(b.user_id) player_qty, a.skullport_flag, a.undermountain_flag, a.plus_one_flag, a.game_id
	                From game a inner join
		                game_played b on a.game_id = b.game_id
	                Group by a.game_id, a.skullport_flag, a.undermountain_flag, a.plus_one_flag
	                ) a
	
                Group by a.player_qty, a.plus_one_flag, a.skullport_flag, a.undermountain_flag
                Order by a.player_qty
                FOR XML Path ('GameType'), ROOT('Leaderboard'), type
                ";
            var sqlQuery = db.Database.SqlQuery<string>(sqlStr).Single();

            XmlDocument newDoc = new XmlDocument();
            newDoc.LoadXml(sqlQuery);
            string newJSON = JsonConvert.SerializeXmlNode(newDoc);

            Response.ContentType = "application/json";
            Response.Write(newJSON);

            return new EmptyResult();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application descrition page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}