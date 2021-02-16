using System;
using System.Collections.Generic;
using System.Text;

namespace Kavan.Entity.Entity
{
    /// <summary>
    /// گروه بندی محصول
    /// </summary>
    public class ProductGroup
    {
        /// <summary>
        /// سازنده
        /// </summary>
        public ProductGroup()
        {
            ProductList = new List<Product>();
        }
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// نام گروه
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// لیست محصول
        /// </summary>
        public virtual List<Product> ProductList { get; set; }
    }
}
