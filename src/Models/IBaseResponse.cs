namespace Bizy.OuinneBiseSharp.Models
{
    using Enums;

    public interface IBaseResponse
    {
        int? ErrorLast { get; set; }
        ErrorLevelEnum ErrorLevel { get; set; }
        int? ErrorsCount { get; set; }
        string ErrorsMsg { get; set; }
        string UserErrorMsg { get; set; }
    }
}