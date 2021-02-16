using Kavan.Areas.Admin.Models;
using Kavan.Entity.Entity;
using Kavan.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kavan.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر محصولات
    /// </summary>
    [Area("Admin")]
    public class ProductController : Controller
    {
        private KavanContext kavanContext;
        public ProductController(KavanContext sc)
        {
            kavanContext = sc;
        }
        /// <summary>
        /// صفحه اصلی
        /// </summary>
        /// <returns></returns>
        [Route("Admin/Product/index")]
        public IActionResult Index()
        {
            var query = kavanContext.Products.Select(x => x);
            var list = query.Select(x => new SearchProductViewModel()
            {
                id = x.Id,
                productTitle = x.Title,
                productFeatureName = x.ProductFeature.Name,
                productGroupName = x.ProductGroup.GroupName,
                price = x.Price,

            }).ToList();
            ViewBag.productList = list;
            return View();
        }
        /// <summary>
        /// صفحه ویرایشس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            ViewBag.Id = id;
            var product = kavanContext.Products.Where(a => a.Id == id).FirstOrDefault();
            ViewBag.ProductFeature = kavanContext.ProductFeatures.ToList();
            return View();
        }
        /// <summary>
        /// صفحه ایجاد
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewBag.productFeatureList = kavanContext.ProductFeatures.ToList().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name + "-" + x.Price });
            ViewBag.ProductFeature = kavanContext.ProductFeatures.ToList();
            ViewBag.productGroup = kavanContext.ProductGroups.ToList();
            return View();
        }

        /// <summary>
        /// صفحه گالری
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Gallery(int id)
        {
            ViewBag.gallery = kavanContext.ProductImages.Where(x => x.ProductId == id).ToList();
            return View();
        }

        /// <summary>
        /// صفحه آپلود فایل
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UploadFile(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        /// <summary>
        /// ایجاد عکس
        /// </summary>
        /// <param name="productImage"></param>
        /// <param name="upload"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateImage(ProductImage productImage, IFormFile upload)
        {

            if (ModelState.IsValid)
            {
                if (upload != null && upload.Length > 0)
                {
                    string ImageName = System.IO.Path.GetFileName(upload.FileName);
                    string ImageText = Path.GetExtension(upload.FileName);
                    if (ImageText == ".png" || ImageText == ".jpg")
                    {
                        string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ImageName);
                        using (var stream = new FileStream(Path.Combine("wwwroot/image", ImageName), FileMode.Create))
                        {
                            upload.CopyTo(stream);
                        }
                    }
                    var entity = new ProductImage
                    {
                        FileId = Guid.NewGuid().ToString().Replace("-", ""),
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        ProductId = productImage.ProductId,
                    };

                    using (var ms = new MemoryStream())
                    {
                        upload.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);

                    }
                    kavanContext.ProductImages.Add(entity);
                    kavanContext.SaveChanges();
                }
                return RedirectToAction("index");
            }
            return View();
        }

        /// <summary>
        /// ذخیره کردن
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Save(SaveProductFeatureVM model)
        {
            if (model.productFeatureId == null)
            {
                return Json(new { isValid = false, message = "ویژگی محصول وارد نشده است" });
            }
            if (model.name == null)
            {
                return Json(new { isValid = false, message = "نام محصول وارد نشده است" });
            }
            if (model.price == null)
            {
                return Json(new { isValid = false, message = "قیمت محصول وارد نشده است" });
            }
            if (model.productGroupId == null)
            {
                return Json(new { isValid = false, message = "گروه بندی محصول وارد نشده است" });
            }

            var item = new Product()
            {
                Title = model.name,
                ProductFeatureId = model.productFeatureId.Value,
                ProductGroupId = model.productGroupId.Value,
                Description = model.description,
                Price = model.price.Value,
            };
            kavanContext.Products.Add(item);
            kavanContext.SaveChanges();
            return Json(new { isValid = true, message = "عملیات ایجاد با موفقیت انجام شد" });
        }
        /// <summary>
        /// عملیات ویرایش
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Update(SaveProductFeatureVM model)
        {

            if (model.productFeatureId == null)
            {
                return Json(new { isValid = false, message = "ویژگی محصول وارد نشده است" });
            }
            if (model.name == null)
            {
                return Json(new { isValid = false, message = "نام محصول وارد نشده است" });
            }
            if (model.price == null)
            {
                return Json(new { isValid = false, message = "قیمت محصول وارد نشده است" });
            }
            if (model.productGroupId == null)
            {
                return Json(new { isValid = false, message = "گروه بندی محصول وارد نشده است" });
            }
            if (model.id > 0 && model.id != null)
            {
                var product = kavanContext.Products.Single(x => x.Id == model.id);
                product.ProductFeatureId = model.productFeatureId.Value;
                product.Title = model.name;
                product.ProductGroupId = model.productGroupId.Value;
                product.Price = model.price.Value;
                product.Description = model.description;
            }

            kavanContext.SaveChanges();
            return Json(new { isValid = true, message = "عملیات ویرایش با موفقیت انجام شد" });
        }
        /// <summary>
        /// صفحه بارگزاری اطلاعات ثبت شده در صفحه برای ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Load(int id)
        {
            Product item;

            item = kavanContext.Products.SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                return Json(
                new
                {
                    isValid = true,
                    data = new SearchProductViewModel
                    {
                        id = item.Id,
                        price = item.Price,
                        description = item.Description,
                        productFeatureId = item.ProductFeatureId,
                        productGroupId = item.ProductGroupId,
                        productTitle = item.Title,
                    }
                });
            }
            else
            {
                return Json(
                    new
                    {
                        isValid = false,
                        message = "محصول مورد نظر یافت نشد."
                    });
            }
        }
        /// <summary>
        /// برگرداندن اطلاعات ویژگی محصولات
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllFeature()
        {
            List<ProductFeature> list;
            list = kavanContext.ProductFeatures.Select(x => x).ToList();
            return Json(
                   new
                   {
                       isValid = true,
                       status = 200,
                       data = new
                       {
                           list = list.Select(item => new
                           {
                               id = item.Id,
                               title = item.Name + "-" + item.Price,
                           })
                       }
                   });
        }
        /// <summary>
        /// برگرداندن اطلاعات گروه بندی محصولات
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllGroup()
        {
            List<ProductGroup> list;
            list = kavanContext.ProductGroups.Select(x => x).ToList();
            return Json(
                   new
                   {
                       isValid = true,
                       status = 200,
                       data = new
                       {
                           list = list.Select(item => new
                           {
                               id = item.Id,
                               title = item.GroupName,
                           })
                       }
                   });
        }
        /// <summary>
        /// حذف محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Product product = kavanContext.Products.Where(x => x.Id == id).FirstOrDefault();
            var image = kavanContext.ProductImages.Where(p => p.ProductId == product.Id).ToList();
            if (image.Count() > 0)
            {
                foreach (var bk in image)
                {
                    var b = bk;
                    kavanContext.ProductImages.Remove(b);
                    kavanContext.SaveChanges();

                }
            }
            kavanContext.Products.Remove(product);
            kavanContext.SaveChanges();
            return Json(new { isValid = true, message = "عملیات حذف با موفقیت انجام شد" });
        }

    }

}

