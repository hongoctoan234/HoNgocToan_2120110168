using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Links")]
    public class Link
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string TypeLink { get; set; }
        public int TableId { get; set; }

    }
}
