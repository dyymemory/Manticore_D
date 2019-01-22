using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.BLL;
using WebApi.Entity.SDTM;
using WebApi.Entity.SDTMComm;

namespace WebApi.Api.Controllers
{
    public class UserModifyController : BaseApiController
    {
        public ResultModel<object> ModifyUserName([FromBody] JObject model )
        {
            var msg = new ResultModel<object>();
            var user = JsonConvert.DeserializeObject<U_User>(JsonConvert.SerializeObject(model));
            if (user == null)
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(user.UserCode))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            return new U_UserBLL().ModifyUserName(user);
        }
    }
}
