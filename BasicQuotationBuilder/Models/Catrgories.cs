namespace BasicQuotationBuilder.Models
{
    public class Catrgories
    {
        public string CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryDescription { get; set; }
        public List<Item> Items { get; set; }
    }
}
