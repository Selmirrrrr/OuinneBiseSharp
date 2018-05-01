namespace Bizy.OuinneBiseSharp.Models
{
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json;

    public class BaseRequest
    {
        [JsonProperty("Method", Order = 1)]
        public string Method { get; set; }

        [JsonProperty("Parameters", Order = 10)]
        public object[] Parameters { get; set; }

        public BaseRequest(object[] parameters = null, [CallerMemberName] string method = null)
        {
            Method = method;
            Parameters = parameters;
        }
    }
}
