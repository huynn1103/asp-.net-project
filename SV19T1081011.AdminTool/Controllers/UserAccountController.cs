using SV19T1081011.BusinessLayers;
using SV19T1081011.DomainModels;
using SV19T1081011.Lib;
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
        private const string USER_ACCOUNT_SEARCH = "UserAccountSearchInput";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session[USER_ACCOUNT_SEARCH] as Models.PaginationSearchInput;
            if (model == null)
            {
                model = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = WebConfig.DefaultPageSize,
                    SearchValue = "",
                };
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            if (input.Page <= 0) input.Page = 1;
            if (input.PageSize <= 0) input.PageSize = WebConfig.DefaultPageSize;
            if (string.IsNullOrEmpty(input.SearchValue)) input.SearchValue = "";

            int rowCount;
            var data = UserAccountService.ListUserAccounts(input.Page,
                                                input.PageSize,
                                                input.SearchValue,
                                                out rowCount);

            Models.UserAccountSearchOutput model = new Models.UserAccountSearchOutput()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[USER_ACCOUNT_SEARCH] = input;
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            long userId = Converter.ToLong(id);
            if (userId == 0)
                return RedirectToAction("Index");

            var model = UserAccountService.GetUserAccount(userId);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật thông tin tài khoản";
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            UserAccount model = new UserAccount()
            {
                UserId = 0,
            };
            ViewBag.Title = "Tạo tài khoản mới";
            return View("Edit", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(UserAccount model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName))
                ModelState.AddModelError(nameof(model.FirstName), "*");
            if (string.IsNullOrWhiteSpace(model.LastName))
                ModelState.AddModelError(nameof(model.LastName), "*");
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError(nameof(model.Email), "*");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError(nameof(model.Phone), "*");
            if (model.UserId == 0)
            {
                if (string.IsNullOrWhiteSpace(model.UserName))
                    ModelState.AddModelError(nameof(model.UserName), "*");
                if (string.IsNullOrWhiteSpace(model.Password))
                    ModelState.AddModelError(nameof(model.Password), "*");
                if (string.IsNullOrWhiteSpace(model.GroupName))
                    ModelState.AddModelError(nameof(model.GroupName), "*");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.UserId == 0 ? "Tạo tài khoản mới" : "Cập nhật thông tin tài khoản";
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin tại các mục có đánh dấu <span class='field-validation-error'>*</span>";
                return View("Edit", model);
            }

            try
            {
                if (model.UserId == 0)
                {
                    model.Password = StringUtils.Md5(model.Password);
                    model.RegisteredTime = DateTime.Now;
                    model.UserId = UserAccountService.AddUserAccount(model);
                }
                else
                {
                    UserAccountService.UpdateUserAccount(model.UserId, model.FirstName, model.LastName, model.Email, model.Phone);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Title = model.UserId == 0 ? "Tạo tài khoản mới" : "Cập nhật tài khoản";
                ViewBag.Message = ex.Message;
                return View("Edit", model);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            long userId = Converter.ToLong(id);
            if (userId == 0)
                return RedirectToAction("Index");

            if (Request.HttpMethod == "POST")
            {
                UserAccountService.DeleteUserAccount(userId);
                return RedirectToAction("Index");
            }

            var model = UserAccountService.GetUserAccount(userId);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}