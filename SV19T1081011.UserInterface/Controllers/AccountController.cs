using SV19T1081011.BusinessLayers;
using SV19T1081011.DomainModels;
using SV19T1081011.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SV19T1081011.UserInterface.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string username = "", string password = "", string categoryUrlName = "", string urlTitle = "")
        {
            Models.UserInterfaceOutput model = new Models.UserInterfaceOutput()
            {
                Categories = ContentService.ListCategories(),
                MostRecentPosts = ContentService.MostRecentList(),
                TrendingPosts = ContentService.TrendingList(),
                FeaturedPosts = ContentService.FeaturedList()
            };

            if (Request.HttpMethod == "POST")
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin!");
                    return View(model);
                }

                var userAccount = UserAccountService.Authorize(username, CryptHelper.EncodeMD5(password));
                if (userAccount == null)
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại!");
                    return View(model);
                }

                WebUserData userData = new WebUserData()
                {
                    UserId = userAccount.UserId.ToString(),
                    UserName = userAccount.UserName,
                    FullName = $"{userAccount.LastName} {userAccount.FirstName}",
                    GroupName = userAccount.GroupName,
                    ClientIP = Request.UserHostAddress,
                    SessionId = Session.SessionID
                };

                FormsAuthentication.SetAuthCookie(userData.ToCookieString(), false);

                if (string.IsNullOrWhiteSpace(categoryUrlName) || string.IsNullOrWhiteSpace(urlTitle))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Post", "Home", new
                    {
                        categoryUrlName = categoryUrlName,
                        urlTitle = urlTitle
                    });
                }
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}