namespace BasicQuotationBuilder.Models.Dtos
{
    public class AddQuotationDto
    {
        public ClientDetails ClientDetails { get; set; }
        public PropertyDetails PropertyDetails { get; set; }
        public QutationMetaData QutationMetaData { get; set; }
        public OrganizationDetails OrganizationDetails { get; set; }
        public List<OrganizationContactDetails> OrganizationContactDetails { get; set; }
    }
}
