using SV19T1081011.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1081011.AdminTool.Models
{
    /// <summary>
    /// Kết quả tìm kiếm, phân trang tài khoản
    /// </summary>
    public class UserAccountSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách tài khoản được hiển thị trên trang
        /// </summary>
        public List<UserAccount> Data { get; set; }
    }
}