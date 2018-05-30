namespace Bizy.OuinneBiseSharp.Models
{
    using Newtonsoft.Json;

    public class PaymentsCalendar : Payments
    {
        [JsonProperty("Term")]
        public string Term { get; set; }
    }
}