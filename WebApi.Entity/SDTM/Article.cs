using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTM
{
    public class Article
    {
        public int ID { get; set; } 
        public string ArticleNo { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string Modifier { get; set; }
        public DateTime ModDate { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string ImageSrc { get; set; }
    }
}
