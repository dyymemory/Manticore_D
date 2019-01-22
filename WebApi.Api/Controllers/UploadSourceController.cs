using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.BLL;
using WebApi.Entity.SDTM;
using WebApi.Entity.SDTM.Enum;
using WebApi.Entity.SDTMComm;

namespace WebApi.Api.Controllers
{
    public class UploadSourceController : BaseApiController
    {
        /// <summary>
        /// 保存图片到服务器
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]//IHttpActionResult
        public ResultModel<dynamic> UploadFormdata([FromBody] JObject Model,[FromUri] Encrypt encrypt)
        {
            var msg= new ResultModel<dynamic>();
            #region
            //var request = System.Web.HttpContext.Current.Request;// 不能用WEBAPI的Request
            //if (request.Files.Count > 0)
            //{
            //    try
            //    {
            //        request.Files[0].SaveAs($"d:/PicUpload/{request.Files[0].FileName}");
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            #endregion
            var picmanage = JsonConvert.DeserializeObject<PicManage>(JsonConvert.SerializeObject(Model));
            if (picmanage == null)
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(picmanage.UserCode))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (picmanage.BaseCodeList.Count == 0)
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            List<Bitmap> bitmaps = new List<Bitmap>();
            var BaseCodeList = picmanage.BaseCodeList;
            bitmaps = FileUpload.Base64ToImg(BaseCodeList);
            var urlList = FileUpload.SavePictureToDoc(bitmaps);
            new ImageBLL().SavePicToDB(picmanage.UserCode, urlList);
            var savemsg = FileUpload.CheckUploadResult(urlList);
            if (!string.IsNullOrEmpty(savemsg))
            {
                msg.Code = 2001;
                msg.Message = savemsg;
            }
            else
            {
                msg.Message = "Success";
            }
            return msg;
        }
        /// <summary>
        /// 获取所有图片
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<List<dynamic>> GetAllPersonalPic([FromBody] JObject Model, [FromUri] PageModel pm)
        {
            ResultModel<List<dynamic>> msg = new ResultModel<List<dynamic>>();
            var picmanage = JsonConvert.DeserializeObject<PicManage>(JsonConvert.SerializeObject(Model));
            if (picmanage == null)
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(picmanage.UserCode))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(picmanage.UserCode))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (true)
            {
                msg = new ImageBLL().GetAllPersonalPic(picmanage,pm);
            }
            else
            {
                msg.Code = 2001;
                msg.Message = "未授权";
            }
            return msg;
        }
        /// <summary>
        /// 保存图片路径到数据库
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<object> SavePicToDB([FromBody] JObject Model,[FromUri] Encrypt encrypt)
        {
            ResultModel<object> msg = new ResultModel<object>();
            var userImage = JsonConvert.DeserializeObject<UserImage>(JsonConvert.SerializeObject(Model));
            if (true)//encrypt.AccessToken == EncrypteComm.Md5Hex32(encrypt.TimeSpan + WebConfigOperation.CommSecretToken)
            {
                msg = new ImageBLL().SavePicToDB(userImage);
            }
            else
            {
                msg.Code = 2001;
                msg.Message = "未授权";
            }
            return msg;
        }
    }
}
