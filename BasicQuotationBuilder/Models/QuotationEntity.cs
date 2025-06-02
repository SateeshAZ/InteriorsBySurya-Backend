using Newtonsoft.Json;

namespace BasicQuotationBuilder.Models
{
    public class QuotationEntity
    {
        [JsonProperty("id")]

        public ClientDetails ClientDetails { get; set; }
        public OrganizationDetails OrganizationDetails { get; set; }
        public List<OrganizationContactDetails> OrganizationContactDetails { get; set; }
        public QutationMetaData QutationMetaData { get; set; }
        public PropertyDetails PropertyDetails { get; set; }
        public Catrgories Catrgories { get; set; }
    }

    
}
