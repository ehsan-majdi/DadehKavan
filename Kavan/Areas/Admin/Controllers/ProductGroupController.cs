using Kavan.Entity.Entity;
using Kavan.Models;
using Kavan.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kavan.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر گروه بندی
    /// </summary>
    [Area("Admin")]
    public class ProductGroupController : Controller
    {
        private KavanContext kavanContext;
        public ProductGroupController(KavanContext sc)
        {
            kavanContext = sc;
        }
        /// <summary>
        /// صفحه اصلی
        /// </summary>
        /// <returns></returns>
        [Route("Admin/ProductGroup/index")]
        public IActionResult Index()
        {
            var list = kavanContext.ProductGroups.ToList();
            ViewBag.productGroupList = list;
            return View();
        }
        /// <summary>
        /// صفحه ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Id = id;
            var productGroup = kavanContext.ProductGroups.Where(a => a.Id == id).FirstOrDefault();
            ViewBag.productGroup = productGroup.GroupName;
            return View();
        }
        /// <summary>
        /// صفحه ایجاد
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// صفحه ایجاد
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProductGroup productGroup)
        {
            if (!ModelState.IsValid)
                return View();
            kavanContext.ProductGroups.Add(productGroup);
            kavanContext.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(ProductGroup productGroup)
        {
            kavanContext.ProductGroups.Update(productGroup);
            kavanContext.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var productGroup = kavanContext.ProductGroups.Where(a => a.Id == id).FirstOrDefault();
            var product = kavanContext.Products.Where(x => x.ProductGroupId == id).FirstOrDefault();
            if (product != null)
            {
                return Json(new { isValid = false , message = "برای این گروه بندی محصولی ثبت شده و شما قادر به حذف گروه بندی نیستید." });
            }
            if (productGroup != null)
            {
                kavanContext.ProductGroups.Remove(productGroup);
                kavanContext.SaveChanges();
                return Json(new { isValid = true , message = "عملیات حذف با موفقیت انجام شد"});

            }
            return null;
        }
    }
}
