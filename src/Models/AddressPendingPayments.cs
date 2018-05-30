namespace Bizy.OuinneBiseSharp.Models
{
    using Newtonsoft.Json;

    public class AddressPendingPayments : AddressPayments
    {
        [JsonProperty("Delay")]
        public string Delay { get; set; }
    }
}