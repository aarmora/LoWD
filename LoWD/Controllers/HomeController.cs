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
            public int undermountain_flag { get; set; }
            public int skullport_flag { get; set; }
            public int plus_one_flag { get; set; }
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

        public ActionResult newGame(string users, JObject gameInfo)
        {
            var gameInfoObject = gameInfo.ToObject<game>();

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
                Select b.user_name, Sum(a.lord_pts + a.gold_pts + a.adv_pts + a.quest_pts - a.corruption_pts) Total
                From game_played a inner join
	                [user] b on a.user_id = b.user_id
                Group By b.user_name
                Order By Sum(a.lord_pts + a.gold_pts + a.adv_pts + a.quest_pts - a.corruption_pts) desc";

            var data = db.Database.SqlQuery<LeaderboardModel>(sqlStr).ToList();

            string json = JsonConvert.SerializeObject(data);
            Response.ContentType = "application/json";
            Response.Write(json);

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