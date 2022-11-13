using SV19T1081011.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081011.AdminTool
{
    /// <summary>
    /// Hỗ trợ các chức năng hiển thị dữ liệu dưới dạng SelectListItem
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Danh sách phân loại tin
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> PostCategories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in ContentService.ListCategories())
            {
                list.Add(new SelectListItem() { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        /// <summary>
        /// Danh sách bài viết
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Posts()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in ContentService.ListPosts())
            {
                list.Add(new SelectListItem() { Value = item.PostId.ToString(), Text = item.Title });
            }
            return list;
        }
    }
}