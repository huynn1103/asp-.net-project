using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1081011.AdminTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class PaginationSearchOutput
    {
        /// <summary>
        /// Trang cần hiển thị
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Số dòng trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Tổng số dòng dữ liệu
        /// </summary>
        public int RowCount { get; set; }
        
        /// <summary>
        ///  Giá trị tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int PageCount
        {
            get {
                if (PageSize == 0)
                    return 1;
                return (RowCount + PageSize - 1) / PageSize;
            }
        }
    }
}