namespace Bizy.OuinneBiseSharp.Models
{
    using System;
    using Enums;
    using Newtonsoft.Json;

    public class AddressPendingPayments
    {
        [JsonProperty("DocumentType")]
        public DocumentTypesEnum DocumentType { get; set; }

        [JsonProperty("DocumentId")]
        public int DocumentId { get; set; }

        [JsonProperty("DocumentNumber")]
        public int DocumentNumber { get; set; }

        [JsonProperty("DocumentDate")]
        public DateTime DocumentDate { get; set; }

        [JsonProperty("AddressId")]
        public int AddressId { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("LocalOpenAmount")]
        public decimal LocalOpenAmount { get; set; }

        [JsonProperty("LocalPaidAmount")]
        public decimal LocalPaidAmount { get; set; }

        [JsonProperty("LocalTotalAmount")]
        public decimal LocalTotalAmount { get; set; }

        [JsonProperty("ForeignOpenAmount")]
        public decimal ForeignOpenAmount { get; set; }

        [JsonProperty("ForeignPaidAmount")]
        public decimal ForeignPaidAmount { get; set; }

        [JsonProperty("ForeignTotalAmount")]
        public decimal ForeignTotalAmount { get; set; }

        [JsonProperty("ForeignCurrency")]
        public string ForeignCurrency { get; set; }

        [JsonProperty("DueDate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("Delay")]
        public string Delay { get; set; }

        [JsonProperty("DocumentCategory")]
        public int DocumentCategory { get; set; }
    }
}