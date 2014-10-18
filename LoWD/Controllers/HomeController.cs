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
                sqlStr1 += " Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {9})";                
                db.Database.ExecuteSqlCommand(sqlStr1, id, that.user_id, that.lord_id, that.lord_pts, that.corruption_pts, that.gold_pts, that.adv_pts, that.quest_pts, that.quest_qty);

                
            }

            return new EmptyResult();
        }

        public ActionResult getInfo()
        {
            string sqlStr = "Select lord_id, name, description From lord";            
            string sqlStr1 = "Select user_id, user_name, fname, lname From [user]";

            var Lords = db.Database.SqlQuery<lord>(sqlStr).ToList();
            var Users = db.Database.SqlQuery<user>(sqlStr1).ToList();

            string newJSON = JsonConvert.SerializeObject(new { lords = Lords, users = Users });
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