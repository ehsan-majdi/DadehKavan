using Kavan.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kavan.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public int id { get; set; }
        public int? productFeatureId { get; set; }
        public int? productGroupId { get; set; }
        public string productTitle { get; set; }
        public string productFeatureName { get; set; }
        public string productGroupName { get; set; }
        public string description { get; set; }
        public long? price { get; set; }
        public string productFeature { get; set; }
        public string productGroup { get; set; }
        public int selectedFeature { get; set; }
        public List<ProductFeature> productFeatures { get; set; }
    }
}
