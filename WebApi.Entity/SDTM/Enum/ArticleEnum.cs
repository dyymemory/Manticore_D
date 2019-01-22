using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Attributes;

namespace WebApi.Entity.SDTM.Enum
{
    public  class ArticleEnum
    {
        public enum ArticleTypeEnum
        {
            /// <summary>
            /// 笔记
            /// </summary>
            [EnumDescribe(Description = "笔记")]
            Note = 1,
            /// <summary>
            /// 随笔
            /// </summary>
            [EnumDescribe(Description = "随笔")]
            Essay = 2,
            /// <summary>
            /// 项目展示
            /// </summary>
            [EnumDescribe(Description = "项目展示")]
            CaseShow = 3
        }
    }
}
