using System;
using System.Collections.Generic;
using System.Text;

namespace Kavan.Entity.Entity
{
    /// <summary>
    /// جدول تصاویر محصول
    /// </summary>
    public class ProductImage /*: BaseEntity<int>*/
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// محصول
        /// </summary>
        public int? ProductId { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// محصول
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
