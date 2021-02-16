using Kavan.Entity.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kavan.Areas.Admin.Models
{
    
    public class SaveProductFeatureVM
    {
        public int? id { get; set; }
        public int? productFeatureId { get; set; }
        public int? productGroupId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public long? price { get; set; }
        public long? featurePrice { get; set; }
    }
    public class SearchProductViewModel
    {
        public int id { get; set; }
        public int? productFeatureId { get; set; }
        public int? productGroupId { get; set; }
        public string productTitle { get; set; }
        public string productFeatureName { get; set; }
        public string productGroupName { get; set; }
        public string description { get; set; }
        public long? price { get; set; }
        public long? featurePrice { get; set; }
        public string productFeature { get; set; }
        public string image { get; set; }
        public string productGroup { get; set; }
        public List<ProductFeature> productFeatures { get; set; }
        public List<ProductImage> productImageList { get; set; }
    }
}
