namespace Bizy.OuinneBiseSharp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Enums;
    using Exceptions;
    using Extensions;
    using Models;
    using Newtonsoft.Json;
    using Refit;

    public class OuinneBiseSharpService
    {
        private readonly IOuinneBiseSharp _apiService;
        private readonly string _appName;
        private readonly int _winBizCompanyId;
        private readonly string _winBizCompanyName;
        private readonly string _winBizKey;
        private readonly string _winBizPassword;
        private readonly string _winBizUsername;
        private readonly int _winBizYear;

        /// <summary>
        ///     OuinneBiseSharpService constructor
        /// </summary>
        /// <param name="winBizCompanyName">
        ///     The name of the Company for which the Cloud subscription has been opened. The
        ///     application must prompt the user for his Company Name.
        /// </param>
        /// <param name="winBizUsername">
        ///     The username of the Cloud user. The username is chosen by the user when he subscribes to
        ///     the service. The application must prompt the user for his username
        /// </param>
        /// <param name="winBizPassword">
        ///     The password of the Cloud user. The password is chosen by the user when he subscribes to
        ///     the service. The application must prompt the user for the password. The password must by encrypted.
        /// </param>
        /// <param name="winBizCompanyId">
        ///     The WinBIZ Folder Number. See this article if you dont know where to find the
        ///     winbiz-companyid. The list of available companies can be obtained via the method <see cref="Folders" />
        /// </param>
        /// <param name="winBizYear">
        ///     The fiscal year of the WinBIZ Folder specified in winbiz-companyid. The list of available
        ///     fiscal years can be obtained via the method Folders.
        /// </param>
        /// <param name="winBizKey">The security key obtained from LOGICIAL SA.</param>
        /// <param name="appName">Your app name, will be displayed in error messages.</param>
        /// <param name="winBizApiUrl">WinBiz Cloud API endpoint URL, defaults to https://api.winbizcloud.ch</param>
        public OuinneBiseSharpService(string winBizCompanyName, string winBizUsername, string winBizPassword, int winBizCompanyId, int winBizYear, string winBizKey, string appName,
            string winBizApiUrl = "https://api.winbizcloud.ch/")
        {
            _winBizCompanyName = winBizCompanyName;
            _winBizUsername = winBizUsername;
            _winBizPassword = winBizPassword.Encrypt();
            _winBizCompanyId = winBizCompanyId;
            _winBizYear = winBizYear;
            _winBizKey = winBizKey;
            _appName = appName;
            _apiService = RestService.For<IOuinneBiseSharp>(winBizApiUrl, new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    Culture = new CultureInfo("fr-CH")
                    {
                        NumberFormat = new NumberFormatInfo
                        {
                            NumberDecimalSeparator = ","
                        }
                    }
                }
            });
        }

        /// <summary>
        ///     This method returns a piece of information about the stock quantities of an item.
        /// </summary>
        /// <param name="cMethod">
        ///     Piece of information to be returned. The possible values for cInfo are in
        ///     <see cref="StockMethodsEnum" />
        /// </param>
        /// <param name="nItem">Item ID (ar_numero).</param>
        /// <param name="dDateEnd">
        ///     The transactions up to this date are selected. This parameter is not mandatory. If the parameter
        ///     is not specified, all the transactions are considered.
        /// </param>
        /// <param name="dDateStart">
        ///     The transactions starting from this date are selected. This parameter is not mandatory. If the
        ///     parameter is not specified, all the transactions are considered.
        /// </param>
        /// <param name="nWarehouse">
        ///     Warehouse ID. If this parameter is specified, only the transactions for this warehouse are
        ///     selected. Otherwise all the transactions are considered.
        /// </param>
        /// <param name="dExpiryEnd">
        ///     Only the transactions concerning line items with an expiry date before the date of the
        ///     parameters are considered. If the parameter is not specified all the transactions are considered.
        /// </param>
        /// <param name="dExpiryStart">
        ///     Only the transactions concerning line items with an expiry date before the date of the
        ///     parameters are considered. If the parameter is not specified all the transactions are considered.
        /// </param>
        /// <returns></returns>
        public async Task<Response<int>> Stock(StockMethodsEnum cMethod, int nItem, DateTime? dDateEnd = null, DateTime? dDateStart = null, int? nWarehouse = null, DateTime? dExpiryEnd = null,
            DateTime? dExpiryStart = null)
        {
            var parameters = new object[]
                {
                    cMethod.ToDescriptionString(), nItem, dDateEnd?.ToOuinneBiseString(), dDateStart?.ToOuinneBiseString(), nWarehouse?.ToString(), dExpiryEnd?.ToOuinneBiseString(),
                    dExpiryStart?.ToOuinneBiseString()
                }
                .AsEnumerable()
                .Where(p => p != null).ToArray();

            return await RequestAsync<Response<int>>(new BaseRequest(parameters));
        }

        /// <summary>
        ///     This method returns the address list.
        /// </summary>
        /// <param name="dDateSince">Allows you to obtain the addresses modified since that date.</param>
        /// <returns>
        ///     <para />
        ///     - the Internal ID of the address (ad_numero)
        ///     <para />
        ///     - the Manual Code of the address (ad_code)
        ///     <para />
        ///     - the Name of the Address (ad_societe)
        ///     <para />
        ///     - the operation performed since the date passed as parameter (operation)
        /// </returns>
        public async Task<Response<List<Address>>> Addresses(DateTime? dDateSince = null)
        {
            var parameters = new object[] { dDateSince?.ToOuinneBiseString() };

            return await RequestAsync<Response<List<Address>>>(new BaseRequest(parameters));
        }

        /// <summary>
        ///     This method can return various information related to an address.
        /// </summary>
        /// <param name="cMethod">
        ///     Piece of information to be returned. The possible values for cInfo are in
        ///     <see cref="AdInfoMethodsEnum" />
        /// </param>
        /// <param name="nAdresse">Only the transactions concerning the addresses with ad_numero = nAdresse are considered.</param>
        /// <param name="dDateEnd">
        ///     The transactions are selected up to the date specified. The parameter is optional.
        ///     If the parameter is missing all the transactions are selected.
        /// </param>
        /// <param name="dDateStart">
        ///     The transactions are selected starting from the date specified. The parameter is optional.
        ///     If the parameter is missing all the transactions are selected
        /// </param>
        /// <param name="vStock">
        ///     This parameter is used only if cInfo is customersalesitem or supplierpurchasesitem.
        ///     If the type of vStock is a string, the cInfo is applied to the Items being in the group specified in vStock.
        /// </param>
        /// <returns></returns>
        public async Task<Response<decimal>> AdInfo(AdInfoMethodsEnum cMethod, int nAdresse, DateTime? dDateEnd = null, DateTime? dDateStart = null, string vStock = null)
        {
            var parameters = new object[] { cMethod.ToDescriptionString(), nAdresse, dDateEnd?.ToOuinneBiseString(), dDateStart?.ToOuinneBiseString(), vStock };

            return await RequestAsync<Response<decimal>>(new BaseRequest(parameters));
        }

        /// <summary>
        ///     <see cref="AdInfo(AdInfoMethodsEnum,int,DateTime?,DateTime?,string)" />
        /// </summary>
        /// <param name="method">
        ///     Piece of information to be returned. The possible values for cInfo are in
        ///     <see cref="AdInfoMethodsEnum" />
        /// </param>
        /// <param name="nAdresse">Only the transactions concerning the addresses with ad_numero = nAdresse are considered.</param>
        /// <param name="vStock">
        ///     This parameter is used only if cInfo is customersalesitem or supplierpurchasesitem.
        ///     If the type of vStock is a numeric, the cInfo is applied to the Item with ar_numero = vStock.
        /// </param>
        /// <param name="dDateEnd">
        ///     The transactions are selected up to the date specified. The parameter is optional.
        ///     If the parameter is missing all the transactions are selected.
        /// </param>
        /// <param name="dDateStart">
        ///     The transactions are selected starting from the date specified. The parameter is optional.
        ///     If the parameter is missing all the transactions are selected
        /// </param>
        /// <returns></returns>
        public async Task<Response<int>> AdInfo(AdInfoMethodsEnum method, int nAdresse, int vStock, DateTime? dDateEnd = null, DateTime? dDateStart = null)
        {
            var parameters = new object[] { method.ToDescriptionString(), nAdresse, dDateEnd?.ToOuinneBiseString(), dDateStart?.ToOuinneBiseString(), vStock };

            return await RequestAsync<Response<int>>(new BaseRequest(parameters));
        }

        /// <summary>
        ///     This method returns a piece of information from a given commercial document.
        /// </summary>
        /// <param name="method">
        ///     Piece of information to be returned. The possible values for cInfo are in
        ///     <see cref="DocInfoMethodsEnum" />
        /// </param>
        /// <param name="dEndDate">
        ///     The transactions are selected up to the date specified. The parameter is optional.
        ///     If the parameter is missing all the transactions are selected.
        /// </param>
        /// <param name="dStartDate">
        ///     The transactions are selected starting from the date specified. The parameter is optional.
        ///     If the parameter is missing all the transactions are selected
        /// </param>
        /// <param name="nComplement">
        ///     Parameter that can be coupled with some of the cMethod parameter.
        ///     Parameter that can be coupled with some of the cMethod parameter.
        ///     <para />
        ///     If cMethod = vente chiffre affaire paiement méthode then it can contain the ID of the payment method of the
        ///     documents (do_ban_pay).
        ///     <para />
        ///     If cMethod = vente chiffre affaire paiement type then it can contain the ID of the type of the payment method of
        ///     the documents (bq_type).
        ///     <para />
        ///     If cMethod = vente chiffre affaire categorie then it can contain the ID of the category of the documents
        ///     (do_catego).
        ///     <para />
        ///     If cMethod = achat montant total categorie then it must contain the ID of the category of the documents
        ///     (do_catego).
        /// </param>
        /// <param name="cComplement">
        ///     Parameter that can be coupled with some of the cMethod parameter.
        ///     <para />
        ///     If cMethod = vente chiffre affaire groupe article then it can contain the items group on which a filter has to be
        ///     applied.
        ///     <para />
        ///     If cMethod = vente marge groupe then it can contain the items group on which a filter has to be applied.
        ///     <para />
        ///     If cMethod = achat montant total groupe article then it can contain the items group on which a filter has to be
        ///     applied.
        /// </param>
        /// <returns></returns>
        public async Task<Response<decimal>> DocInfo(DocInfoMethodsEnum method, DateTime dEndDate, DateTime dStartDate, int? nComplement = null, string cComplement = null)
        {
            var parameters = new object[] { method.ToDescriptionString(), dEndDate.ToOuinneBiseString(), dStartDate.ToOuinneBiseString(), nComplement, cComplement };

            return await RequestAsync<Response<decimal>>(new BaseRequest(parameters));
        }

        /// <summary>
        ///     This method returns the number of the open (unpaid) documents along with the open (unpaid) amount in the local
        ///     currency.
        ///     The results grouped by document type and payment delay. Up to three different delays can be passed as a parameter.
        /// </summary>
        /// <param name="nFirstIntervalLimit">
        ///     This parameter allows to specify a first group of late payments.
        ///     The method will return the documents for which the payments are late, up to the number of days specified in the
        ///     parameter.
        ///     The delay is calculated from the due date. If documents with multiple due dates exist, each due date is taken into
        ///     consideration.
        /// </param>
        /// <param name="nSecondIntervalLimit">
        ///     This parameter allows to specify a first group of late payments.
        ///     The interval taken into consideration is the one between nFirstIntervalLimit+1 and the number of days passed as
        ///     parameter here.
        /// </param>
        /// <param name="nThirdIntervalLimit">
        ///     Third limit to set up the delay intervals.
        ///     The method returns all open documents with a delay between the second and third interval limit, and all open
        ///     documents with a delay greater than this interval limit.
        /// </param>
        /// <returns>
        ///     Return all pending payments for the intervals
        ///     <para />
        ///     Remark: The method will also always return the payments that have a delay bigger than the bigger delay specified in
        ///     the parameters
        /// </returns>
        public async Task<Response<List<PendingPayments>>> PendingPayments(int nFirstIntervalLimit, int? nSecondIntervalLimit = null, int? nThirdIntervalLimit = null)
        {
            var parameters = new object[] { nFirstIntervalLimit, nSecondIntervalLimit, nThirdIntervalLimit };

            return await RequestAsync<Response<List<PendingPayments>>>(new BaseRequest(parameters));
        }

        /// <summary>
        ///     This method returns the number of the open (unpaid) documents along with the open (unpaid) amount in the local
        ///     currency.
        ///     The results are grouped by address of the document, document type and payment delay.
        /// </summary>
        /// <param name="nDelayTo">
        ///     This parameter allows to specify the maximal delay of the payments included in the results.
        ///     The delay is calculated from the due date. If documents with multiple due dates exist, each due date is taken into
        ///     consideration.
        /// </param>
        /// <param name="nDelayFrom">
        ///     This parameter allows to specify the minimal delay of the payments included in the results.
        ///     <para />
        ///     If the parameter is omitted, all the documents with a delay smaller than the one specified in nDelayTo will be
        ///     included.
        /// </param>
        /// <returns>All pending payments for the intervals</returns>
        public async Task<Response<List<AddressesPendingPayments>>> AddressesPendingPayments(int nDelayTo, int? nDelayFrom = null)
        {
            var parameters = new object[] { nDelayTo, nDelayFrom };

            return await RequestAsync<Response<List<AddressesPendingPayments>>>(new BaseRequest(parameters));
        }

        /// <summary>
        ///     This method returns the detail of all open documents for a given address for a specific delay interval.
        /// </summary>
        /// <param name="nAddressId">Id of the address</param>
        /// <param name="nFirstIntervalLimit">
        ///     First limit to set up the delay interval. The method returns all open documents with
        ///     a delay up to this interval limit.
        /// </param>
        /// <param name="nSecondIntervalLimit">
        ///     Second limit to set up the delay interval. The method returns all open documents with a delay greater than this
        ///     interval limit.
        ///     If nFirstIntervalLimit is set, the delay interval will be all open documents between the second and the first
        ///     limit.
        /// </param>
        /// <returns></returns>
        public async Task<Response<List<AddressPendingPayments>>> AddressPendingPayments(int nAddressId, int nFirstIntervalLimit, int? nSecondIntervalLimit = null)
        {
            var parameters = new object[] { nAddressId, nFirstIntervalLimit, nSecondIntervalLimit };

            return await RequestAsync<Response<List<AddressPendingPayments>>>(new BaseRequest(parameters));
        }

        /// <summary>
        ///     This method returns the number of the due documents along with due amount in the local currency.
        ///     The results grouped by document type and payment due date. Up to three different ranges can be passed as a
        ///     parameter to filter the documents.
        /// </summary>
        /// <param name="nFirstDueRange">
        ///     This parameter allows to specify a first range to filter on the due date.
        ///     The method will return the documents for which the payments are due in the days between today and today +
        ///     nFrstDueRange.
        ///     If documents with multiple due dates exist, each due date is taken into consideration separetely.
        /// </param>
        /// <param name="nSecondDueRange">
        ///     This parameter allows to specify a second range to filter on the due date.
        ///     The range taken into consideration is the one between nFirstDueTerm+1 and the number of days passed as parameter
        ///     here.
        ///     All the documents with a due date comprised in that range will be grouped together.
        /// </param>
        /// <param name="nThirdDueRange">
        ///     Third range of to filter on the due date.
        ///     The method returns all the due documents with a due date between the second and third due range, and all the open
        ///     documents with a due date greater than today + nThirdDue Range.
        /// </param>
        /// <remarks>
        ///     The method will also always return the payments that have a due date bigger than the bigger due term specified
        ///     in the parameters.
        /// </remarks>
        public async Task<Response<object>> PaymentsCalendar(int nFirstDueRange, int? nSecondDueRange = null, int? nThirdDueRange = null)
        {
            var parameters = new object[] { nFirstDueRange, nSecondDueRange, nThirdDueRange };

            throw new NotImplementedException();
        }

        /// <summary>
        ///     This method returns the number of the due documents along with the due amount in the local currency. The results
        ///     are grouped by address of the document, document type and payment due date.
        /// </summary>
        /// <param name="nDueTo">
        ///     The documents with a due date between today and today + nDueTo will be insluded in the results.
        ///     If documents with multiple due dates exist, each due date is taken into consideration separately.
        /// </param>
        /// <param name="nDueFrom">The documents with a due date greater than today + nDueFrom will be included in the results.</param>
        /// <returns></returns>
        public async Task<Response<object>> AddressesPaymentsCalendar(int nDueTo, int? nDueFrom = null)
        {
            var parameters = new object[] { nDueTo, nDueFrom };

            throw new NotImplementedException();
        }

        /// <summary>
        ///     This method returns the detail of all open documents for a given address for due date that is comprised in a given
        ///     range of days.
        /// </summary>
        /// <param name="nAddressId">Id of the address</param>
        /// <param name="nFirstDueRange">
        ///     First limit to set up the due date range. The method returns all open documents with due
        ///     date up to today + nFirstDueRange.
        /// </param>
        /// <param name="nSecondDueRange">
        ///     Second limit to set up the due date range. The method returns all open documents with a
        ///     due date greater than today + nSecondDueRange.
        /// </param>
        /// <returns></returns>
        public async Task<string> AddressPaymentsCalendar(int nAddressId, int nFirstDueRange, int? nSecondDueRange = null)
        {
            var parameters = new object[] { nAddressId, nFirstDueRange, nSecondDueRange };

            throw new NotImplementedException();
        }

        /// <summary>
        ///     This method returns the folders list.
        ///     The Headers "winbiz-companyid" and "winbiz-year" are compulsory, but can be left empty when this method is used.
        ///     The method can be used as an alternative way to prompt the user with a list of folders / fiscal year that replaces
        ///     the need to enter manually this values at the logon.
        /// </summary>
        /// <returns>
        ///     Returns the complete list of the folders that are available in the user's Winbiz Cloud, no matter the content
        ///     of the "winbiz-companyid" and "winbiz-year" headers.
        /// </returns>
        public async Task<Response<List<Dossier>>> Folders()
        {
            return await RequestAsync<Response<List<Dossier>>>(new BaseRequest());
        }

        private async Task<T> RequestAsync<T>(BaseRequest request) where T : IBaseResponse
        {
            try
            {
                var result = await _apiService.Req<T>(request, _winBizCompanyName, _winBizUsername, _winBizPassword, _winBizCompanyId, _winBizYear, _winBizKey).ConfigureAwait(false);

                switch (result.ErrorLast ?? 0)
                {
                    case 109:
                    case 202:
                    case 239:
                    case 297:
                    case 490:
                    case 514:
                    case 734:
                    case 933:
                    case 963:
                    case 981:
                        result.ErrorLevel = ErrorLevelEnum.Developer;
                        result.UserErrorMsg = $"An error occurred in the application {_appName}";
                        break;
                    case 111:
                    case 134:
                    case 197:
                    case 250:
                    case 280:
                    //case 297:
                    case 299:
                    case 327:
                    case 420:
                    //case 514:
                    case 535:
                    case 666:
                    case 667:
                    case 668:
                    case 672:
                    case 673:
                    case 689:
                    case 717:
                    case 737:
                    case 837:
                    case 864:
                    case 905:
                    case 999:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 193:
                        result.ErrorLevel = ErrorLevelEnum.Customer;
                        result.UserErrorMsg = "Some of the following login details are missing: Name of the Company, ID of the Folder, Fiscal Year";
                        break;
                    case 314:
                        result.ErrorLevel = ErrorLevelEnum.Customer;
                        result.UserErrorMsg = "One of the login details is wrong or missing";
                        break;
                    case 803:
                        result.ErrorLevel = ErrorLevelEnum.Customer;
                        result.UserErrorMsg = "One of the login details is wrong or missing";
                        break;
                    case 335:
                        result.ErrorLevel = ErrorLevelEnum.Customer;
                        result.UserErrorMsg = $"This user is not authorized to use the application {_appName}";
                        break;
                    case 670:
                        result.ErrorLevel = ErrorLevelEnum.Customer;
                        result.UserErrorMsg = "The company selected by the user can't be found.";
                        break;
                }

                return result;
            }
            catch (Exception e)
            {
                throw new OuinneBiseSharpException("Bad request.", e);
            }
        }
    }
}