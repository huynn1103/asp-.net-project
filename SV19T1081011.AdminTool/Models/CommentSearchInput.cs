using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1081011.AdminTool.Models
{
    /// <summary>
    /// Đầu vào dùng cho tìm kiếm, phân trang đối với comment
    /// </summary>
    public class CommentSearchInput : PaginationSearchInput
    {
        /// <summary>
        /// Comment bài viết cần tìm (0 nếu tìm tất cả bài viết)
        /// </summary>
        public long PostId { get; set; }
    }
}