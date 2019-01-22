using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL;
using WebApi.Entity.SDTM;
using WebApi.Entity.SDTMComm;

namespace WebApi.BLL
{
    public class ImageBLL
    {
        public ResultModel<List<dynamic>> GetAllPersonalPic(PicManage picManage,PageModel pm)
        {
            ResultModel<List<dynamic>> msg = new ResultModel<List<dynamic>>();
            msg = new ResultModel<List<dynamic>>() { Data = new ImageDAL().GetAllPersonalPic(picManage, pm), PM = pm };
            if (msg == null || msg.Data.Count == 0)
            {
                msg.Message = "暂无数据";
            }
            return msg;
        }
        /// <summary>
        /// 修改个人头像
        /// </summary>
        /// <param name="userImage"></param>
        /// <returns></returns>
        public ResultModel<object> SavePicToDB(UserImage userImage)
        {
            ResultModel<object> msg = new ResultModel<object>();
            int result = 0;
            userImage.ImageCode = new CreateEmpCode().GetRandomEmpCode(2, 4);
            result = new ImageDAL().SavePicToDB(userImage);
            if (result > 0)
            {
                msg.Message = "操作成功";
            }
            else
            {
                msg.Code = 2001;
                msg.Message = "操作失败";
            }
            return msg;
        }
        /// <summary>
        /// 图片上传到服务器并保存到数据库
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="urlList"></param>
        public void SavePicToDB(string UserCode,List<string> urlList)
        {
            foreach (var item in urlList)
            {

            }
        }
    }
}
