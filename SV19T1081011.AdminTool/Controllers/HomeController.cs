using SV19T1081011.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081011.AdminTool.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public string Test(string s)
        {
            // Chuyển tiếng Việt có dấu thành không dấu
            s = s.ToUnSign().ToLower();
            // Chuyển tất cả các ký tự đặc biệt thành -
            s = Regex.Replace(s, "[^a-z0-9]+", "-");
            //// Chuyển tất cả -- thành -
            //s = Regex.Replace(s, "--", "-");
            //// Chuyển -- thành - lần cuối cùng
            //s = s.Replace("--", "-");
            //// Loại bỏ - thừa 2 đầu
            //s = s.Trim('-');
            return s;
        }
    }
}