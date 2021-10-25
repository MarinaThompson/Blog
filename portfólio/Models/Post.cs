using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portfólio.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Subtitle { get; set; }

        public string Image { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public int UserID { get; set; } 
    }
}
