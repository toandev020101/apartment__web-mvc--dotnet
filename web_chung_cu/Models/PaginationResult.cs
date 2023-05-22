using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_chung_cu.Models
{
    public class PaginationResult<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}