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
    public  class ArticleDAL
    {
        public List<dynamic> GetUserArticle(ArticleModel article, PageModel pm)
        {
            //            string sql = @"SELECT  * ,
            //        SrcString = STUFF(( SELECT   ',' + img.ImageSrc
            //                               FROM     dbo.Image img
            //                               WHERE    img.ImageCode = art.ImageCode
            //                             FOR
            //                               XML PATH('')
            //                             ), 1, 1, '')
            //FROM    dbo.Article art;";
            //            StringBuilder sbSql = new StringBuilder(@"SELECT  A.* ,
            //        I.ImageSrc
            //FROM    dbo.Article A
            //        LEFT JOIN dbo.Image I 
            //        ON I.ImageCode = A.ImageCode
            //WHERE   A.IsDel = 0");
            StringBuilder sbSql = new StringBuilder(@"SELECT  A.* 
FROM    dbo.Article A
");
            var dyParamter = new DynamicParameters();
            dyParamter.Add("PageIndex", pm.PageIndex);
            dyParamter.Add("PageSize", pm.PageSize);
            string querySql = string.Format("WITH query AS ({0}) ", sbSql);
            string countSql = querySql + " SELECT COUNT(*) FROM query";

            using (var conn = AdoConfig.GetDBConnection())
            {
                pm.TotalCount = conn.Query<int>(countSql, dyParamter).FirstOrDefault();
                if (pm.TotalCount > 0)
                {
                    string pageSql = querySql + " SELECT * FROM ( SELECT ROW_NUMBER() OVER(ORDER BY ID ASC) AS RowNum, * FROM query ) t WHERE t.RowNum > (@PageIndex -1) * @PageSize AND t.RowNum <= @PageIndex * @PageSize";
                    return conn.Query<dynamic>(pageSql, dyParamter).ToList();
                }
                return new List<dynamic>();
            }
        }
        public int UpdateArticle(Article article, UserInfoForCookie user)
        {
            StringBuilder sbsql = new StringBuilder(@"UPDATE  dbo.Article
        SET ");
            var dyParamter = new DynamicParameters();
            dyParamter.Add("UserCode", user.UserCode);
            dyParamter.Add("ArticleNo", article.ArticleNo);
            if (article.Title != null)
            {
                sbsql.Append("Title=@Title");
                dyParamter.Add("Title", article.Title);
            }
            if (article.Content != null)
            {
                sbsql.Append("Content=@Content");
                dyParamter.Add("Content", article.Content);
            }
            if (article.ImageUrl != null)
            {
                sbsql.Append("ImageUrl=@ImageUrl");
                dyParamter.Add("ImageUrl", article.ImageUrl);
            }
            sbsql.Append(@"
                Modifier = @UserCode ,
                ModDate = GETDATE()
        WHERE   ArticleNo = @ArticleNo");
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Execute(sbsql.ToString(),dyParamter);
            }
        }
        public int InsertArticle(Article article, UserInfoForCookie user)
        {
            string sql = @" INSERT dbo.Article
                ( Title ,
                  ArticleNo,
                  Creator ,
                  CreateDate ,
                  Content ,
                  ImageUrl
                )
        VALUES  ( @Title , 
                  @ArticleNo,
                  @UserCode , 
                  GETDATE() , 
                  @Content ,
                  @ImageUrl
                )";
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Execute(sql, new { article.Title, article.ArticleNo, article.Content,article.ImageUrl, user.UserCode });
            }
        }
        public int DeleteArticle(Article article, UserInfoForCookie user)
        {
            string sql = @"UPDATE  dbo.Article
        SET     IsDel = 1 ,
                Modifier = @UserCode ,
                ModDate = GETDATE()
        WHERE   ArticleNo = @ArticleNo;";
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Execute(sql, new { article.ArticleNo, user.UserCode });
            }
        }
        public List<dynamic> GetFollowMessage(PageModel pm)
        {
            StringBuilder sbSql = new StringBuilder(@"SELECT TOP 2000
        pro.PropertyID ,
        pro.PropertyCode ,
        fol.FollowID ,
        fol.FollowCode ,
        fol.FollowDate ,
        fol.FollowType ,
        fol.DeptName + '.' + fol.EmpName AS EmpName ,
        pro.PropertyNo ,
        est.EstateName ,
        pro.TradeInt ,
        fol.Content ,
        fol.AlertDate ,
        fol.AlertInfo ,
        fol.AlertType ,
        fol.FlagAlerted
FROM    Property AS pro ( NOLOCK )
        LEFT JOIN Follow AS fol ( NOLOCK ) ON fol.PropertyCode = pro.PropertyCode
        LEFT JOIN Estate AS est ( NOLOCK ) ON pro.EstateCode = est.EstateCode
WHERE   fol.FlagDeleted = 0
        AND fol.FlagTrashed = 0
        AND pro.IsDel = 0 
");
            var dyParamter = new DynamicParameters();
            dyParamter.Add("PageIndex", pm.PageIndex);
            dyParamter.Add("PageSize", pm.PageSize);
            string querySql = string.Format("WITH query AS ({0}) ", sbSql);
            string countSql = querySql + " SELECT COUNT(*) FROM query";

            using (var conn = AdoConfig.GetDBConnection())
            {
                pm.TotalCount = conn.Query<int>(countSql, dyParamter).FirstOrDefault();
                if (pm.TotalCount > 0)
                {
                    string pageSql = querySql + " SELECT * FROM ( SELECT ROW_NUMBER() OVER(ORDER BY FollowID ASC) AS RowNum, * FROM query ) t WHERE t.RowNum > (@PageIndex -1) * @PageSize AND t.RowNum <= @PageIndex * @PageSize";
                    return conn.Query<dynamic>(pageSql, dyParamter).ToList();
                }
                return new List<dynamic>();
            }
        }
    }
}
