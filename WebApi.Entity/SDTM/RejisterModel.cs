using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTM
{
    public class RejisterModel : ResetPasswordModel
    {
        public string UserName { get; set; }
        public bool Type { get; set; } = true;
    }
}
