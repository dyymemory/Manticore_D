using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTM
{
    public class ArticleReturn<T>
    {
        public string Title { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string Modifier { get; set; }
        public DateTime ModDate { get; set; }
        public string Content { get; set; }
        public string ImageCode { get; set; }
        public string ArticleNo { get; set; }
        public T ImageSrcList { get; set; }
    }
}
