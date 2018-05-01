namespace Bizy.OuinneBiseSharp.Models
{
    using Newtonsoft.Json;

    public class Dossier
    {
        [JsonProperty("dos_numero")]
        public long Number { get; set; }

        [JsonProperty("dos_name")]
        public string Name { get; set; }

        [JsonProperty("exercices")]
        public Exercice[] Exercices { get; set; }
    }
}