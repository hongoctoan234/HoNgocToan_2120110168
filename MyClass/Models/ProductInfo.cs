using System;
using System.ComponentModel.DataAnnotations;

namespace MyClass.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string Slug { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Images { get; set; }
        public string Detail { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKey { get; set; }
        public int? CreatedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }
        public DateTime? SaleStart { get; set; }
        public DateTime? SaleEnd { get; set; }
        public int Status { get; set; }

    }
}
