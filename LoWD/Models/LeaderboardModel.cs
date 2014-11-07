using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoWD.Models
{
    public class LeaderboardModel
    {
        public string user_name { get; set; }
        public int total { get; set; }
        public decimal avgQ {get; set;}
        public int games_played { get; set; }
        public int avgptsgame { get; set; }
    }
}