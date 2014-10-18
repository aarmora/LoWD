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
            var gameInfoJson = gameInfo.ToObject<game>();

            JArray objects = JArray.Parse(users);
            foreach (JObject item in objects)
            {
                var that = item.ToObject<game_played>();

                Response.Write(that.lord_pts);
                
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