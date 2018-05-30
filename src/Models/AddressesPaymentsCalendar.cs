namespace Bizy.OuinneBiseSharp.Models
{
    using Newtonsoft.Json;

    public class AddressesPaymentsCalendar : AddressesPayments
    {
        [JsonProperty("Term")]
        public string Term { get; set; }
    }
}