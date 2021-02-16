using Kavan.Entity.Entity;
using Kavan.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kavan.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر ویژگی محصولات
    /// </summary>
    [Area("Admin")]
    public class ProductFeatureController : Controller
    {
        private KavanContext kavanContext;
        public ProductFeatureController(KavanContext sc)
        {
            kavanContext = sc;
        }
        /// <summary>
        /// صفحه اصلی
        /// </summary>
        /// <returns></returns>
        [Route("Admin/ProductFeature/index")]
        public IActionResult Index()
        {
            var list = kavanContext.ProductFeatures.ToList();
            ViewBag.productFeaturesList = list;
            return View();
        }
        /// <summary>
        /// صفحه ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            ViewBag.Id = id;
            var productFeature = kavanContext.ProductFeatures.Where(a => a.Id == id).FirstOrDefault();
            ViewBag.Name = productFeature.Name;
            ViewBag.Price = productFeature.Price;
            return View();
        }
        /// <summary>
        /// ایجاد کردن
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// ایجاد کردن
        /// </summary>
        /// <param name="productFeature"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProductFeature productFeature)
        {
            if (!ModelState.IsValid)
                return View();
            kavanContext.ProductFeatures.Add(productFeature);
            kavanContext.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="productFeature"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(ProductFeature productFeature)
        {
            kavanContext.ProductFeatures.Update(productFeature);
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
            var productFeatures = kavanContext.ProductFeatures.Where(a => a.Id == id).FirstOrDefault();
            var product = kavanContext.Products.Where(x => x.ProductFeatureId == id).FirstOrDefault();
            if (product != null)
            {
                return Json(new { isValid = false, message = "برای این ویژگی محصولی ثبت شده و شما قادر به حذف گروه بندی نیستید." });
            }
            if (productFeatures != null)
            {
                kavanContext.ProductFeatures.Remove(productFeatures);
                kavanContext.SaveChanges();
                return Json(new { isValid = true, message = "عملیات حذف با موفقیت انجام شد" });
            }
            return null;
        }
    }
}
