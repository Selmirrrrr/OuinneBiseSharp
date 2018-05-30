namespace Bizy.OuinneBiseSharp.Models
{
    using Newtonsoft.Json;

    public class AddressPaymentsCalendar : AddressPayments
    {
        [JsonProperty("Term")]
        public string Term { get; set; }
    }
}