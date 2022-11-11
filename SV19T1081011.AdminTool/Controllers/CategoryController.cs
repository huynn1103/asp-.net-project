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
    public class CategoryController : Controller
    {
        private const string CATEGORY_SEARCH = "CategorySearchInput";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            Models.PaginationSearchInput model = Session[CATEGORY_SEARCH] as Models.PaginationSearchInput;
            if (model == null)
            {
                model = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = WebConfig.DefaultPageSize,
                    SearchValue = ""
                };
            }

            return View(model);

            //int rowCount = 0, pageSize = 5, pageCount = 1;
            //var model = ContentService.ListCategories(page, pageSize, searchValue, out rowCount);
            //pageCount = (rowCount + pageSize - 1) / pageSize;

            //ViewBag.RowCount = rowCount;
            //ViewBag.PageCount = pageCount;
            //ViewBag.Page = page;
            //ViewBag.SearchValue = searchValue;

            //return View(model);
        }

        /// <summary>
        /// Tìm kiếm, hiển thị phân loại tin
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            if (input.Page <= 0)
                input.Page = 1;
            if (input.PageSize <= 0)
                input.PageSize = WebConfig.DefaultPageSize;
            if (string.IsNullOrEmpty(input.SearchValue))
                input.SearchValue = "";

            int rowCount;
            var data = ContentService.ListCategories(input.Page, 
                                                    input.PageSize,
                                                    input.SearchValue,
                                                    out rowCount);
            Models.CategorySearchOutput model = new Models.CategorySearchOutput()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[CATEGORY_SEARCH] = input; //Lưu lại đầu vào tìm kiếm

            return View(model);
        }

        public ActionResult ChangeOrder(int id, bool moveUp)
        {
            ContentService.ChangeCategoryOrder(id, moveUp);
            return Json("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Cập nhật phân loại tin";

            int categoryId = Converter.ToInt(id);
            if (categoryId == 0)
                return RedirectToAction("Index");
            
            var model = ContentService.GetCategory(categoryId);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveData(PostCategory model)
        {
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                ModelState.AddModelError("CategoryName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.CategoryUrlName))
                ModelState.AddModelError("UrlName", "Tên trên URL không được để trống");
            if (ContentService.ExistsCategoryUrlName(model.CategoryUrlName, model.CategoryId))
                ModelState.AddModelError("UrlName", "Giá trị này bị trùng");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CategoryId > 0 ? "Cập nhật phân loại tin" : "Bổ sung phân loại tin";
                return View("Edit", model);
            }

            if (model.CategoryId > 0)
            {
                bool updateResult = ContentService.UpdateCategory(model);
            }
            else
            {
                ContentService.AddCategory(model);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung phân loại tin";
            PostCategory model = new PostCategory()
            {
                CategoryId = 0
            };

            return View("Edit", model);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            ViewBag.Title = "Xác nhận xóa phân loại tin";

            int categoryId = 0;
            bool result = int.TryParse(id, out categoryId);
            if (!result)
                return RedirectToAction("Index");

            if (Request.HttpMethod == "POST")
            {
                ContentService.DeleteCategory(categoryId);
                return RedirectToAction("Index");
            }

            var model = ContentService.GetCategory(categoryId);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
    }
}