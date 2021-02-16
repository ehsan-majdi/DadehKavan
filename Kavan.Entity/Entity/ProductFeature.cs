using System;
using System.Collections.Generic;
using System.Text;

namespace Kavan.Entity.Entity
{
    /// <summary>
    /// ویژگی محصول
    /// </summary>
    public class ProductFeature
    {
        /// <summary>
        /// سازنده
        /// </summary>
        public ProductFeature()
        {
            ProductList = new List<Product>();
        }
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// نام ویژگی
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// قیمت مربوط به ویژگی
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// لیست محصول
        /// </summary>
        public virtual List<Product> ProductList { get; set; }
    }
}
