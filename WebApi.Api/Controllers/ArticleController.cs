using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.BLL;
using WebApi.Entity.SDTM;
using WebApi.Entity.SDTMComm;

namespace WebApi.Api.Controllers
{
    //[EnableCors(origins: "http://localhost:8080/", headers: "*", methods: "GET,POST,PUT,DELETE")]
    public class ArticleController : BaseApiController
    {
        [HttpPost]
        public ResultModel<List<dynamic>> GetUserArticle([FromBody] JObject model , [FromUri] PageModel pm)
        {
            ResultModel<List<dynamic>> msg = new ResultModel<List<dynamic>>();
            var article= JsonConvert.DeserializeObject<ArticleModel>(JsonConvert.SerializeObject(model));
            if (pm.PageIndex == 0 || pm.PageCount == 0)
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (true)
            {
                msg = new ArticleBLL().GetUserArticle(article,pm);
            }
            else
            {
                msg.Code = 2001;
                msg.Message = "未授权";
            }
            return msg;
        }
        [HttpPost]
        public ResultModel<object> OperateArticle([FromBody] JObject model, [FromUri] Encrypt encrypt)
        {
            var article= JsonConvert.DeserializeObject<Article>(JsonConvert.SerializeObject(model));
            ResultModel<object> msg = new ResultModel<object>();
            if (true)//encrypt.AccessToken == EncrypteComm.Md5Hex32(encrypt.TimeSpan + WebConfigOperation.CommSecretToken)
            {
                msg= new ArticleBLL().OperateArticle(article,User); 
            }
            else
            {
                msg.Code = 2001;
                msg.Message = "未授权";
            }
            return msg;
        }
        [HttpPost]
        public ResultModel<object> DeleteArticle([FromBody] JObject model)
        {
            var article= JsonConvert.DeserializeObject<Article>(JsonConvert.SerializeObject(model));
            ResultModel<object> msg = new ResultModel<object>();
            if (true)//encrypt.AccessToken == EncrypteComm.Md5Hex32(encrypt.TimeSpan + WebConfigOperation.CommSecretToken)
            {
                msg = new ArticleBLL().DeleteArticle(article, User);
            }
            else
            {
                msg.Code = 2001;
                msg.Message = "未授权";
            }
            return msg;
        }
        [HttpPost]
        public ResultModel<List<dynamic>> GetFollowMessage([FromUri] PageModel pm)
        {
            ResultModel<List<dynamic>> msg = new ResultModel<List<dynamic>>();
            msg = new ArticleBLL().GetFollowMessage(pm);
            return msg;
        }
    }
}
