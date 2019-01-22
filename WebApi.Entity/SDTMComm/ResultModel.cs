using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTMComm
{
    public class ResultModel<T>
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回状态 2000表示成功
        /// </summary>
        private int code = 2000;
        public int Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }
        /// <summary>
        /// 返回实体对象
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public PageModel PM { get; set; }
    }
}
