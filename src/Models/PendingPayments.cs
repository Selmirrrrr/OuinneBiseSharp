namespace Bizy.OuinneBiseSharp.Models
{
    using Enums;
    using Newtonsoft.Json;

    public class PendingPayments
    {
        [JsonProperty("DocumentType")]
        public DocumentTypesEnum DocumentType { get; set; }

        [JsonProperty("Delay")]
        public string Delay { get; set; }

        [JsonProperty("OpenDocuments")]
        public int OpenDocuments { get; set; }

        [JsonProperty("LocalOpenAmount")]
        public decimal LocalOpenAmount { get; set; }
    }
}