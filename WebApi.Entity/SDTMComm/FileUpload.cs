using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTMComm
{
    public class FileUpload
    {
        /// <summary>
        /// base64转化成bitmap
        /// </summary>
        /// <param name="BaseCodeList"></param>
        /// <returns></returns>
        public static List<Bitmap> Base64ToImg(List<string> BaseCodeList)
        {
            List<Bitmap> bitmaps = new List<Bitmap>();
            foreach (var item in BaseCodeList)
            {
                byte[] bt = Convert.FromBase64String(item);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(bt);
                Bitmap bitmap = new Bitmap(stream);
            }
            return bitmaps;
        }
        /// <summary>
        /// 保存bitmap到服务器
        /// </summary>
        /// <param name="bitmaps"></param>
        /// <returns></returns>
        public static List<string> SavePictureToDoc(List<Bitmap> bitmaps)
        {
            List<string> urlList = new List<string>();
            for (int i = 0; i < bitmaps.Count; i++)
            {
                string picsrc = "d:/PicUpload/" + new CreateEmpCode().GetRandomEmpCode(10, 5) + ".jpg";
                try
                {
                    bitmaps[i].Save(picsrc, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    urlList.Add((i + 1).ToString());
                    break;
                }
                urlList.Add(picsrc);
            }       
            return urlList;
        }
        /// <summary>
        /// 检查上传结果
        /// </summary>
        /// <param name="urlList"></param>
        /// <returns></returns>
        public static string CheckUploadResult(List<string> urlList)
        {
            string resultmessage = "";
            foreach (var item in urlList)
            {
                try
                {
                    var result = Convert.ToInt32(item);
                    resultmessage += "第" + result + "张上传失败;";
                    urlList.Remove(item);
                }
                catch
                {
                    break;
                }
            }
            return resultmessage;
        }
    }
}
