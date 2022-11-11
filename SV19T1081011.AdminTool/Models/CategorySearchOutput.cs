using SV19T1081011.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1081011.AdminTool.Models
{
    /// <summary>
    /// Kết quả tìm kiếm, phân trang loại tin
    /// </summary>
    public class CategorySearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách loại tin được hiển thị trên trang
        /// </summary>
        public List<PostCategory> Data { get; set; }
    }
}