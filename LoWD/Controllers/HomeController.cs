using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoWD.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace LoWD.Controllers
{
    public class HomeController : Controller
    {
        private LoWDEntities db = new LoWDEntities();

        public class game
        {
            public int? game_id { get; set; }
            public int? user_id { get; set; }
            public int? lord_id { get; set; }
            public int? lord_pts { get; set; }
            public int? corruption_pts { get; set; }
            public int? gold_pts { get; set; }
            public int? adv_pts { get; set; }
            public int? quest_pts { get; set; }
            public int? quest_qty { get; set; }
        }


        public class lord
        {
            public int? lord_id { get; set; }
            public int? name { get; set; }
            public int? description { get; set; }
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult newGame(string users, string gameInfo)
        {

            JArray objects = JArray.Parse(users);
            foreach (JObject item in objects)
            {
                var that = item.ToObject<game>();

                Response.Write(that.lord_pts);
                
            }


            return new EmptyResult();
        }

        public ActionResult getInfo()
        {
            //string sqlStr = "Select * From lowd.lord";

            //var Lords = db.Database.SqlQuery<lord>(sqlStr).ToList();

            //string newJSON = JsonConvert.SerializeObject(Lords);

            //Response.Write(newJSON);

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