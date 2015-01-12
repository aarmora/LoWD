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
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;

namespace LoWD.Controllers
{
    public class tournamentsController : Controller
    {
        // GET: tournaments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult createTourney(string tournament)
        {
            JObject jo = JObject.Parse(tournament);
            Response.Write(jo["name"]);

            // + "&type=" + jo["type"] +  "&description=" +  jo["description"]

            WebRequest webRequest = WebRequest.Create("https://api.challonge.com/v1/tournaments.json?api_key=hoQa7CcPNOJDTIuRlTja23OPEHril7QvIPqApLtG&tournament[name]="+ jo["name"] + "&tournament[url]=" + jo["url"] );            
            webRequest.Method = "POST";
            WebResponse webResp = webRequest.GetResponse();
            StreamReader stream = new System.IO.StreamReader(webResp.GetResponseStream());

            return new EmptyResult();
        }


        public ActionResult getChallongeDesc(int tourney)
        {
         
            WebRequest webRequest = WebRequest.Create("https://api.challonge.com/v1/tournaments/cpsLowd_" + tourney + ".json?api_key=hoQa7CcPNOJDTIuRlTja23OPEHril7QvIPqApLtG");
            webRequest.ContentType = "application/json";
            WebResponse webResp = webRequest.GetResponse();
            StreamReader stream = new System.IO.StreamReader( webResp.GetResponseStream());

            Response.Write(stream.ReadToEnd());

            return new EmptyResult();
        }
    }
}