using System;
using System.ComponentModel.DataAnnotations;

namespace MyClass.Models
{
    public class OrderInfor
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExportDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryEmail { get; set; }
        public string DeliveryNote { get; set; }
        public int? UpdatedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}
