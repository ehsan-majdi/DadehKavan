using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kavan.Entity.Entity
{
    /// <summary>
    /// جدول محصول
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف ویژگی
        /// </summary>
        public int ProductFeatureId { get; set; }
        /// <summary>
        /// ردیف گروه بندی
        /// </summary>
        public int ProductGroupId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [MaxLength(1000)]
        public string Description { get; set; }
        /// <summary>
        /// قیمت محصول
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// گروه بندی محصول
        /// </summary>
        public virtual ProductGroup ProductGroup { get; set; }
        /// <summary>
        /// ویژگی محصول
        /// </summary>
        public virtual ProductFeature ProductFeature { get; set; }
        /// <summary>
        /// لیست تصاویر محصول
        /// </summary>
        public virtual List<ProductImage> Images { get; set; }
    }
}
