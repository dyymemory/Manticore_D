using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTM
{
    public class PicManage
    {
        public string UserCode { get; set; }
        public List<string> BaseCodeList { get; set; }
        public string TimeSpan { get; set; }
        public string AccessToken { get; set; }
    }
}
