namespace Bizy.OuinneBiseSharp.Models
{
    using Newtonsoft.Json;

    public class PendingPayments : Payments
    {
        [JsonProperty("Delay")]
        public string Delay { get; set; }
    }
}