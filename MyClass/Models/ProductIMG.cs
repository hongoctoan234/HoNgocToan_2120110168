using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("ProductIMGs")]
    public class ProductIMG
    {
        [Key]
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdOption { get; set; }
        public string Images { get; set; }
    }
}
