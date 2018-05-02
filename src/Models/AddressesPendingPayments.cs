namespace Bizy.OuinneBiseSharp.Models
{
    using Enums;
    using Newtonsoft.Json;

    public class AddressesPendingPayments
    {
        [JsonProperty("DocumentType")]
        public DocumentTypesEnum DocumentType { get; set; }

        [JsonProperty("AddressId")]
        public int AddressId { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Delay")]
        public string Delay { get; set; }

        [JsonProperty("OpenDocuments")]
        public int OpenDocuments { get; set; }

        [JsonProperty("LocalOpenAmount")]
        public decimal LocalOpenAmount { get; set; }
    }
}