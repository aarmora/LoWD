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

        public class user
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult newGame(string users)
        {
            //turn the array of json objects into the user model.

            //This works for a single user.  (it would be sent from the js without the [] and we'd catch it over here as just a string)
            //var json = JsonConvert.DeserializeObject<user>(users);

            var json = users;
            JArray objects = JArray.Parse(json);

            int count = 0;
            foreach (JObject item in objects)
            {
                foreach (KeyValuePair<String, JToken> app in item)
                {
                    var appName = app.Key;
                    var val = app.Value;

                    Response.Write(appName + ": " + app.Value + "<br> ");
                    //var description = (String)app.Value["Description"];
                    //var value = (String)app.Value["Value"];

                    //Console.WriteLine(appName);
                    //Console.WriteLine(description);
                    //Console.WriteLine(value);
                    //Console.WriteLine("\n");
                }
               // count = count + 1;

                //Response.Write(count);

               // Response.Write(item);
                
            }


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