﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTM
{
    public class ArticleModel: Article
    {
        public string TimeSpan { get; set; }
        public string AccessToken { get; set; }
    }
}
