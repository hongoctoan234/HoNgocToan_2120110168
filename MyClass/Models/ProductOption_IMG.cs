namespace MyClass.Models
{
    public class ProductOption_IMG
    {
        public int Id { get; set; }
        public int IdOption { get; set; }
        public int IdProduct { get; set; }
        public string OptionName { get; set; }
        public int Count { get; set; }
        public string Images { get; set; }
        public string ImageAvt { get; set; }
    }
}
