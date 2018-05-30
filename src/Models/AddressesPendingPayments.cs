namespace Bizy.OuinneBiseSharp.Models
{
    using Newtonsoft.Json;

    public class AddressesPendingPayments : AddressesPayments
    {
        [JsonProperty("Delay")]
        public string Delay { get; set; }
    }
}