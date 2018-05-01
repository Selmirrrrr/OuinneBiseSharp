namespace Bizy.OuinneBiseSharp.Services
{
    using System.Threading.Tasks;
    using Bizy.OuinneBiseSharp.Models;
    using Refit;

    public interface IOuinneBiseSharp
    {
        [Post("/Bizinfo")]
        Task<T> Req<T>([Body] BaseRequest req,
            [Header("winbiz-companyname")] string winbizCompanyName,
            [Header("winbiz-username")] string winbizUsername,
            [Header("winbiz-password")] string winbizPassword,
            [Header("winbiz-companyid")] int winbizCompanyId,
            [Header("winbiz-year")] int winbizYear,
            [Header("winbiz-key")] string winbizKey
        );
    }
}