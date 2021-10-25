using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portfólio.ModelViews
{
    public class PostViewModel
    {
        public int ID { get; set; }
        public string Subtitle { get; set; }
        public string CurrentImage { get; set; } = "";
        public IFormFile Image { get; set; } = null;
        public int UserID { get; set; }
    }
}
