using SV19T1081011.BusinessLayers;
using SV19T1081011.DomainModels;
using SV19T1081011.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SV19T1081011.AdminTool.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var userData = WebUserData.FromCookie(User.Identity.Name);
            var userAccount = UserAccountService.GetUserAccount(userData.UserName);

            return View(userAccount);
        }

        /// <summary>
        /// Xử lý điều hướng nếu chưa đăng nhập hoặc không đúng quyền
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult NotAuthorize()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string username = "", string password = "")
        {
            if (Request.HttpMethod == "POST")
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Nhập thông tin!");
                    return View();
                }

                var userAccount = UserAccountService.Authorize(username, CryptHelper.EncodeMD5(password));
                if (userAccount == null)
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại!");
                    return View();
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
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit()
        {
            ViewBag.Title = "Chỉnh sửa thông tin cá nhân";

            var userData = WebUserData.FromCookie(User.Identity.Name);
            var userAccount = UserAccountService.GetUserAccount(userData.UserName);

            return View(userAccount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserAccount model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName))
                ModelState.AddModelError("FirstName", "Họ và tên đệm không được để trống");
            if (string.IsNullOrWhiteSpace(model.LastName))
                ModelState.AddModelError("LastName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", "Email không được để trống");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "Số điện thoại không được để trống");

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            UserAccountService.UpdateUserAccount(model.UserId, model.FirstName, model.LastName, model.Email, model.Phone);

            WebUserData userData = new WebUserData()
            {
                UserId = model.UserId.ToString(),
                UserName = model.UserName,
                FullName = $"{model.FirstName} {model.LastName}",
                GroupName = model.GroupName,
                ClientIP = Request.UserHostAddress,
                SessionId = Session.SessionID

            };
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(userData.ToCookieString(), false);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "Đổi mật khẩu";

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string userId, string userName, string oldPassword, string newPassword, string againNewPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword))
                ModelState.AddModelError("oldPassword", "Mật khẩu cũ không được để trống");
            if (string.IsNullOrWhiteSpace(newPassword))
                ModelState.AddModelError("newPassword", "Mật khẩu mới không được để trống");
            if (string.IsNullOrWhiteSpace(againNewPassword))
                ModelState.AddModelError("againNewPassword", "Nhập lại mật khẩu mới không được để trống");
            if (!ModelState.IsValid)
            {
                return View("ChangePassword");
            }

            if (newPassword != againNewPassword)
                ModelState.AddModelError("againNewPassword", "Nhập lại mật khẩu mới không đúng");
            if (oldPassword == newPassword)
                ModelState.AddModelError("newPassword", "Mật khẩu mới trùng mật khẩu cũ");
            if (!ModelState.IsValid)
            {
                return View("ChangePassword");
            }

            if (UserAccountService.ChangePassword(userName, CryptHelper.EncodeMD5(oldPassword), CryptHelper.EncodeMD5(newPassword)))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("oldPassword", "Mật khẩu cũ không đúng");
                return View("ChangePassword");
            }
        }
    }
}