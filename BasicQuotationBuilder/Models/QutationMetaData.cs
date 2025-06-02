namespace BasicQuotationBuilder.Models
{
    public class QutationMetaData
    {
        public string QuotationId { get; set; }
        public string QutationType { get; set; }
        public string QutationTitle { get; set; }
        public string QuotationStatus { get; set; }
        public string QuotationScopeOfWork { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public DateTimeOffset QuotationValidaity { get; set; }
    }
}
