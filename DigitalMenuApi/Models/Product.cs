
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductListProduct = new HashSet<ProductListProduct>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string Src { get; set; }
        public int? StoreId { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<ProductListProduct> ProductListProduct { get; set; }
    }
}