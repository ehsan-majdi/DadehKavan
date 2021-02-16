using Kavan.Areas.Admin.Models;
using Kavan.Models;
using Kavan.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Kavan.Controllers
{
    /// <summary>
    /// کنترلر هوم سایت
    /// </summary>
    public class HomeController : Controller
    {
        private KavanContext kavanContext;
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, KavanContext sc)
        {
            _logger = logger;
            kavanContext = sc;
        }
        /// <summary>
        /// صفحه اصلی
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// صفحه مشاهده محصولات
        /// </summary>
        /// <returns></returns>
        public IActionResult Shop()
        {
            var query = kavanContext.Products.Select(x => x);
            var list = query.Select(x => new SearchProductViewModel()
            {
                id = x.Id,
                productTitle = x.Title,
                productFeatureName = x.ProductFeature.Name,
                productGroupName = x.ProductGroup.GroupName,
                featurePrice = x.ProductFeature.Price,
                price = x.Price,
                image = x.Images.Select(y => y.FileName).FirstOrDefault(),

            }).ToList();
            ViewBag.productList = list;
            return View();
        }
        /// <summary>
        /// صفحه مشاهده جزئیات هر محصول بر اساس کلید اصلی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Product(int id)
        {
            ViewBag.gallery = kavanContext.ProductImages.Where(x => x.ProductId == id).ToList();
            var query = kavanContext.Products.Select(x => x);
            var list = query.Select(x => new SearchProductViewModel()
            {
                id = x.Id,
                productTitle = x.Title,
                productFeatureName = x.ProductFeature.Name,
                productGroupName = x.ProductGroup.GroupName,
                featurePrice = x.ProductFeature.Price,
                price = x.Price,
                image = x.Images.Select(y => y.FileName).FirstOrDefault(),
            }).ToList();
            ViewBag.productList = list;
            return View();
        }
    }
}
