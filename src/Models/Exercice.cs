namespace Bizy.OuinneBiseSharp.Models
{
    using System;
    using Newtonsoft.Json;

    public class Exercice
    {
        [JsonProperty("exc_annee")]
        public int Year { get; set; }

        [JsonProperty("exc_debut")]
        public DateTime Start { get; set; }

        [JsonProperty("exc_fin")]
        public DateTime End { get; set; }

        [JsonProperty("exc_desc")]
        public string Description { get; set; }

        [JsonProperty("exc_isbcl")]
        public bool IsClosed { get; set; }
    }
}