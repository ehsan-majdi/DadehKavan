using System;
using System.Collections.Generic;
using System.Text;
using Kavan.Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace Kavan.Persistence.Context
{
    public class KavanContext : DbContext
    {
        public KavanContext(DbContextOptions<KavanContext> options) : base(options) { }
        /// <summary>
        /// جدول محصول
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// جدول ویژگی
        /// </summary>
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        /// <summary>
        /// جدول گروه
        /// </summary>
        public DbSet<ProductGroup> ProductGroups { get; set; }
        /// <summary>
        /// جدول تصاویر
        /// </summary>
        public DbSet<ProductImage> ProductImages { get; set; }


    }
}
