using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Coupons")]
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CouponCode { get; set; }
        public int? CreatedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CouponStart { get; set; }
        public DateTime? CouponEnd { get; set; }
        public int Status { get; set; }
    }
}
