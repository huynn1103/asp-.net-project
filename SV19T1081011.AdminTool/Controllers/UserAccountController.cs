using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081011.AdminTool.Controllers
{
    [Authorize(Roles = WebAccountRoles.ADMINISTRATOR)]
    public class UserAccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}