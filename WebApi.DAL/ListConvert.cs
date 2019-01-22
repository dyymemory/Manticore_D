using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.SDTM;

namespace WebApi.DAL
{
    public class ListConvert
    {
        //public List<ArticleReturn<List<Image>>> GetListModel(List<Article> articles)
        //{
        //    var groups = articles.GroupBy(p => p.ID).ToList();
        //    var ListModel = new List<ArticleReturn<List<Image>>>();
        //    foreach (var group in groups)
        //    {
        //        var EntityModel = new ArticleReturn<List<Image>>();
        //        var ImageList = new List<Image>();            
        //        foreach (var item in group)
        //        {
        //            var ImageEntity = new Image();
        //            ImageEntity.ImageSrc = item.ImageSrc;
        //            ImageList.Add(ImageEntity);
        //            EntityModel.ImageSrcList = ImageList;
        //            EntityModel.ArticleNo = item.ArticleNo;
        //            EntityModel.Title = item.Title;
        //            EntityModel.Creator = item.Creator;
        //            EntityModel.CreateDate = item.CreateDate;
        //            EntityModel.Modifier = item.Modifier;
        //            EntityModel.ModDate = item.ModDate;
        //            EntityModel.Content = item.Content;
        //        }
        //        ListModel.Add(EntityModel);
        //    }
        //    return ListModel;
        //}
    }
}
