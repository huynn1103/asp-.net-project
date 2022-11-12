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
    public class CommentSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public long PostId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PostComment> Data { get; set; }
    }
}