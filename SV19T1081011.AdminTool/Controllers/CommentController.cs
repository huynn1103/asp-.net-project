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
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = WebAccountRoles.ADMINISTRATOR)]
    public class CommentController : Controller
    {
        private const string COMMENT_SEARCH = "CommentSearchInput";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.CommentSearchInput model = Session[COMMENT_SEARCH] as Models.CommentSearchInput;
            if (model == null)
            {
                model = new Models.CommentSearchInput()
                {
                    Page = 1,
                    PageSize = WebConfig.DefaultPageSize,
                    SearchValue = "",
                    PostId = 0
                };
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.CommentSearchInput input)
        {
            if (input.Page <= 0) input.Page = 1;
            if (input.PageSize <= 0) input.PageSize = WebConfig.DefaultPageSize;
            if (string.IsNullOrEmpty(input.SearchValue)) input.SearchValue = "";

            int rowCount;
            var data = ContentService.ListComments(input.Page,
                                                input.PageSize,
                                                input.SearchValue,
                                                input.PostId,
                                                out rowCount);

            Models.CommentSearchOutput model = new Models.CommentSearchOutput()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                PostId = input.PostId,
                RowCount = rowCount,
                Data = data
            };
            Session[COMMENT_SEARCH] = input;
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            long commentId = Converter.ToLong(id);
            if (commentId == 0)
                return RedirectToAction("Index");

            var model = ContentService.GetComment(commentId);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật bình luận";
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var user = this.User.GetUserData();
            PostComment model = new PostComment()
            {
                CommentId = 0,
            };
            ViewBag.Title = "Nhập bình luận mới";
            return View("Edit", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(PostComment model)
        {
            if (string.IsNullOrWhiteSpace(model.CommentContent))
                ModelState.AddModelError(nameof(model.CommentContent), "*");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CommentId == 0 ? "Nhập bài viết mới" : "Cập nhật bài viết";
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin tại các mục có đánh dấu <span class='field-validation-error'>*</span>";
                return View("Edit", model);
            }

            try
            {
                if (model.CommentId == 0)
                {
                    model.CreatedTime = DateTime.Now;
                    model.UserId = Converter.ToLong(this.User.GetUserData().UserId);
                    model.CommentId = ContentService.AddComment(model);
                }
                else
                {
                    ContentService.UpdateComment(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Title = model.PostId == 0 ? "Nhập bình luận mới" : "Cập nhật bình luận";
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
            long commentId = Converter.ToLong(id);
            if (commentId == 0)
                return RedirectToAction("Index");

            if (Request.HttpMethod == "POST")
            {
                ContentService.DeleteComment(commentId);
                return RedirectToAction("Index");
            }

            var model = ContentService.GetComment(commentId);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}