namespace BasicQuotationBuilder.Models
{
    public class QuotationTemplate
    {
        public string QuotationTemplateTitle { get; set; }
        public string QuotationTemplateId { get; set; }
        public string QuotationTemplateDescription { get; set; }
        public DateTimeOffset QuotationTemplateCreatedAt { get; set; }
        public DateTimeOffset QuotationTemplateModifiedAt { get; set; }
        public Catrgories Catrgories { get; set; }
    }
}
