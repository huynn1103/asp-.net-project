using SV19T1081011.BusinessLayers;
using SV19T1081011.DomainModels;
using SV19T1081011.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081011.AdminTool.Controllers
{
    [Authorize(Roles = WebAccountRoles.ADMINISTRATOR)]
    public class PostController : Controller
    {
        private const string POST_SEARCH = "PostSearchInput";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PostSearchInput model = Session[POST_SEARCH] as Models.PostSearchInput;
            if (model == null)
            {
                model = new Models.PostSearchInput()
                {
                    Page = 1,
                    PageSize = WebConfig.DefaultPageSize,
                    SearchValue = "",
                    CategoryId = 0
                };
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PostSearchInput input)
        {
            if (input.Page <= 0) input.Page = 1;
            if (input.PageSize <= 0) input.PageSize = WebConfig.DefaultPageSize;
            if (string.IsNullOrEmpty(input.SearchValue)) input.SearchValue = "";

            int rowCount;
            var data = ContentService.ListPosts(input.Page,
                                                input.PageSize,
                                                input.SearchValue,
                                                input.CategoryId,
                                                out rowCount);

            Models.PostSearchOutput model = new Models.PostSearchOutput()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                CategoryId = input.CategoryId,
                RowCount = rowCount,
                Data = data
            };
            Session[POST_SEARCH] = input;
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            long postId = Converter.ToLong(id);
            if (postId == 0)
                return RedirectToAction("Index");

            var model = ContentService.GetPost(postId);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật bài viết";
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var user = this.User.GetUserData();
            Post model = new Post()
            {
                PostId = 0,
                Image = "Uploads/PostImages/no-image.png"
            };
            ViewBag.Title = "Nhập bài viết mới";
            return View("Edit", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    string path = Server.MapPath("~/Uploads/PostImages");
                    string fileName = $"{DateTime.Now.Ticks}_{file.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);
                    file.SaveAs(filePath);
                    return $"Uploads/PostImages/{fileName}";
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(Post model, HttpPostedFileBase uploadImage)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
                ModelState.AddModelError(nameof(model.Title), "*");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.PostId == 0 ? "Nhập bài viết mới" : "Cập nhật bài viết";
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin tại các mục có đánh dấu <span class='field-validation-error'>*</span>";
                return View("Edit", model);
            }

            if (string.IsNullOrWhiteSpace(model.UrlTitle))
                model.UrlTitle = GenerateUrlTitle(model.Title);
            if (string.IsNullOrWhiteSpace(model.FullContent))
                model.FullContent = "";

            string imageFile = UploadFile(uploadImage);
            if (imageFile != "")
                model.Image = imageFile;

            try
            {
                if (model.PostId == 0)
                {
                    model.CreatedTime = DateTime.Now;
                    model.UserId = Converter.ToLong(this.User.GetUserData().UserId);
                    model.PostId = ContentService.AddPost(model);
                }
                else
                {
                    ContentService.UpdatePost(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Title = model.PostId == 0 ? "Nhập bài viết mới" : "Cập nhật bài viết";
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
            long postId = Converter.ToLong(id);
            if (postId == 0)
                return RedirectToAction("Index");

            if (Request.HttpMethod == "POST")
            {
                ContentService.DeletePost(postId);
                return RedirectToAction("Index");
            }

            var model = ContentService.GetPost(postId);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Preview(string id)
        {
            var model = ContentService.GetPost(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        /// <summary>
        /// Tự tạo UrlTitle dựa trên title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GenerateUrlTitle(string title)
        {
            title = $"{Regex.Replace(title.ToUnSign().ToLower(), "[^a-z0-9]+", "-")}-{DateTime.Now.Ticks}";
            title = title.Replace("--", "-");
            return title;
        }
    }
}