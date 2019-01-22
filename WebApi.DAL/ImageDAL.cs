using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.SDTM;
using WebApi.Entity.SDTMComm;

namespace WebApi.DAL
{
    public  class ImageDAL
    {
        public List<dynamic> GetAllPersonalPic(PicManage picManage, PageModel pm)
        {
            StringBuilder sbSql = new StringBuilder(@"SELECT * FROM dbo.Image WHERE UserCode=@UserCode");
            var dyParamter = new DynamicParameters();
            dyParamter.Add("UserCode", picManage.UserCode);
            dyParamter.Add("PageIndex", pm.PageIndex);
            dyParamter.Add("PageSize", pm.PageSize);
            string querySql = string.Format("WITH query AS ({0}) ", sbSql);
            string countSql = querySql + " SELECT COUNT(*) FROM query";

            using (var conn = AdoConfig.GetDBConnection())
            {
                try
                {
                    pm.TotalCount = conn.Query<int>(countSql, dyParamter).FirstOrDefault();
                    if (pm.TotalCount > 0)
                    {
                        string pageSql = querySql + " SELECT * FROM ( SELECT ROW_NUMBER() OVER(ORDER BY IsDel ASC) AS RowNum, * FROM query ) t WHERE t.RowNum > (@PageIndex -1) * @PageSize AND t.RowNum <= @PageIndex * @PageSize";
                        return conn.Query<dynamic>(pageSql, dyParamter).ToList();
                    }
                }
                catch(Exception ex)
                {
                    new SendWarnningEMail().SendEMail(ex.ToString());
                }
                return new List<dynamic>();
            }
        }
        public int SavePicToDB(UserImage userImage)
        {
            string sql = @"UPDATE  dbo.Image
SET IsDel = 1,ModDate = GETDATE()
WHERE UserCode = @UserCode AND IsDel=0";
            sql += @"UPDATE  dbo.Image
SET IsDel = 0,ModDate = GETDATE()
WHERE UserCode = @UserCode AND ImageCode=@ImageCode
;";
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Execute(sql, new { ImageCode = userImage.ImageCode, ImageSrc= userImage.ImageSrc, UserCode=userImage.UserCode, });
            }
        }
        public void SavePicToDB(string UserCode,List<string>urlList)
        {
            StringBuilder InsertSql = new StringBuilder();
            DynamicParameters dyParameters = new DynamicParameters();
            dyParameters.Add("UserCode", UserCode);
            for (int i= 0;i < urlList.Count;i++)
            {
                InsertSql.AppendFormat(@"INSERT  dbo.Image
        ( ImageCode, ImageSrc, UserCode,IsDel )
VALUES  ( @ImageCode{0}, @ImageSrc{0}, @UserCode,1 );", i);
                dyParameters.Add("ImageSrc" + i, urlList[i]);
                dyParameters.Add("ImageCode" + i, new CreateEmpCode().GetRandomEmpCode(2, 4));
            }
            using (var conn = AdoConfig.GetDBConnection())
            {
                conn.Execute(InsertSql.ToString(), dyParameters);
            }
        }
    }
}
