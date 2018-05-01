namespace Bizy.OuinneBiseSharp.Models
{
    using Newtonsoft.Json;

    public class Address
    {
        [JsonProperty("ad_numero")]
        public int AdNumero { get; set; }
        [JsonProperty("ad_code")]
        public string AdCode { get; set; }
        [JsonProperty("ad_societe")]
        public string AdSociete { get; set; }
    }
}