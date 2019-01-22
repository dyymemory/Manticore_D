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
    public class ArticleBLL
    {
        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns></returns>
        public ResultModel<List<dynamic>> GetUserArticle(ArticleModel article, PageModel pm)
        {
            ResultModel<List<dynamic>> msg = new ResultModel<List<dynamic>>();
            //List<dynamic> articles= new ArticleDAL().GetUserArticle(article,pm);
            msg = new ResultModel<List<dynamic>>() { Data = new ArticleDAL().GetUserArticle(article, pm), PM = pm };
            //msg.Data = new ListConvert().GetListModel(articles);
            if (msg == null || msg.Data.Count == 0)
            {
                msg.Message = "暂无数据";
            }
            return msg;
        }
        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public ResultModel<object> OperateArticle(Article article,UserInfoForCookie user)
        {
            ResultModel<object> msg = new ResultModel<object>();
            if (article == null)
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (article.Title == null && article.Content == null && article.ImageUrl == null)
            {
                msg.Code = 2000;
                msg.Message = "未作修改";
                return msg;
            }
            int result = 0;
            if (!string.IsNullOrEmpty(article.ArticleNo))//update
            {                
                result = new ArticleDAL().UpdateArticle(article, user);
            }
            else//insert
            {
                article.ArticleNo = new CreateEmpCode().GetRandomEmpCode(2,4);
                result = new ArticleDAL().InsertArticle(article, user);
            }
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
        public ResultModel<object> DeleteArticle(Article article, UserInfoForCookie user)
        {
            ResultModel<object> msg = new ResultModel<object>();
            if (article == null)
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(article.ArticleNo))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            int result = 0;
            result = new ArticleDAL().DeleteArticle(article, user);
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
        public ResultModel<List<dynamic>> GetFollowMessage(PageModel pm)
        {
            ResultModel<List<dynamic>> msg = new ResultModel<List<dynamic>>();
            msg.Data = new ArticleDAL().GetFollowMessage(pm);
            msg.PM = pm;
            return msg;
        }
    }
}
