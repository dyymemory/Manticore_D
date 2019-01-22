using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTMComm
{
    /// <summary>
    /// 分页
    /// </summary>
    public class PageModel
    {
        /// <summary>
        /// 总笔数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 默认当前页数为一页 计算总页数
        /// </summary>
        private int pageCount = 1;
        public int PageCount
        {
            get
            {
                if (TotalCount >= 1)
                {
                    pageCount = (int)Math.Ceiling(float.Parse(TotalCount.ToString()) / pageSize);
                }
                return pageCount;
            }
            set { pageCount = value; }
        }
        /// <summary>
        ///当前页索引
        /// </summary>
        private int pageIndex;
        public int PageIndex
        {
            get
            {
                if (pageIndex == 0)
                {
                    return 1;
                }
                return pageIndex;
            }
            set { pageIndex = value; }
        }
        /// <summary>
        /// 每页显示笔数
        /// </summary>
        private int pageSize = 20;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
    }
}
