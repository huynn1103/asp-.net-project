using SV19T1081011.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1081011.AdminTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PostSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Post> Data { get; set; }
    }
}