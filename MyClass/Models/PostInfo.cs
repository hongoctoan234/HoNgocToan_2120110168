using System;
using System.ComponentModel.DataAnnotations;

namespace MyClass.Models
{
    public class PostInfo
    {
        public int Id { get; set; }
        public int TopId { get; set; }
        public string TopicName { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Images { get; set; }
        public string Detail { get; set; }
        public string Type { get; set; }
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
        public int Status { get; set; }
    }
}
