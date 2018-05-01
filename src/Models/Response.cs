namespace Bizy.OuinneBiseSharp.Models
{
    using Enums;
    using Newtonsoft.Json;

    public class Response<T> : IBaseResponse
    {
        [JsonProperty("ErrorsCount")]
        public int? ErrorsCount { get; set; }

        [JsonProperty("ErrorLast")]
        public int? ErrorLast { get; set; }

        [JsonProperty("ErrorsMsg")]
        public string ErrorsMsg { get; set; }

        public string UserErrorMsg { get; set; }

        public ErrorLevelEnum ErrorLevel { get; set; }

        [JsonProperty("Value")]
        public T Value { get; set; }
    }
}