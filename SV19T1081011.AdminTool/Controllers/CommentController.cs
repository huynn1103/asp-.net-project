using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081011.AdminTool.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = WebAccountRoles.MODERATOR)]
    public class CommentController : Controller
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