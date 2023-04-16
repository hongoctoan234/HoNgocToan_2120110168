﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("ProductOptions")]
    public class ProductOption
    {
        [Key]
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public string OptionName { get; set; }
        public int Count { get; set; }
    }
}
