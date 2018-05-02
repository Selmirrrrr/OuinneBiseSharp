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
        private readonly OuinneBiseApiSettings _ouinneBiseApiSettings;
        private readonly string _appName;
        private readonly int _companyId;
        private readonly int _year;
        private readonly IOuinneBiseSharp _apiService;

        /// <summary>
        /// OuinneBiseSharpService constructor
        /// </summary>
        /// <param name="ouinneBiseApiSettings"><see cref="OuinneBiseApiSettings"/></param>
        /// <param name="appName">Name of your final application, will be displayed in error messages.</param>
        /// <param name="companyId">User's company ID</param>
        /// <param name="year">Selected fiscal year</param>
        public OuinneBiseSharpService(OuinneBiseApiSettings ouinneBiseApiSettings, string appName, int companyId, int year)
        {
            _ouinneBiseApiSettings = ouinneBiseApiSettings;
            _appName = appName;
            _companyId = companyId;
            _year = year;
            _apiService = RestService.For<IOuinneBiseSharp>(ouinneBiseApiSettings.Url, new RefitSettings
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
        /// This method returns a piece of information about the stock quantities of an item.
        /// </summary>
        /// <param name="cMethod">Piece of information to be returned. The possible values for cInfo are in <see cref="StockMethodsEnum"/></param>
        /// <param name="nItem">Item ID (ar_numero).</param>
        /// <param name="dDateEnd">The transactions up to this date are selected. This parameter is not mandatory. If the parameter is not specified, all the transactions are considered.</param>
        /// <param name="dDateStart">The transactions starting from this date are selected. This parameter is not mandatory. If the parameter is not specified, all the transactions are considered.</param>
        /// <param name="nWarehouse">Warehouse ID. If this parameter is specified, only the transactions for this warehouse are selected. Otherwise all the transactions are considered.</param>
        /// <param name="dExpiryEnd">Only the transactions concerning line items with an expiry date before the date of the parameters are considered. If the parameter is not specified all the transactions are considered.</param>
        /// <param name="dExpiryStart">Only the transactions concerning line items with an expiry date before the date of the parameters are considered. If the parameter is not specified all the transactions are considered.</param>
        /// <returns></returns>
        public async Task<Response<int>> Stock(StockMethodsEnum cMethod, int nItem, DateTime? dDateEnd = null, DateTime? dDateStart = null, int? nWarehouse = null, DateTime? dExpiryEnd = null, DateTime? dExpiryStart = null)
        {
            var parameters = new object[]
                    {cMethod.ToDescriptionString(), nItem, dDateEnd?.ToOuinneBiseString(), dDateStart?.ToOuinneBiseString(), nWarehouse?.ToString(), dExpiryEnd?.ToOuinneBiseString(), dExpiryStart?.ToOuinneBiseString()}
                .AsEnumerable()
                .Where(p => p != null).ToArray();

            return await RequestAsync<Response<int>>(new BaseRequest(parameters));
        }

        /// <summary>
        /// This method returns the address list.
        /// </summary>
        /// <param name="dDateSince">Allows you to obtain the addresses modified since that date.</param>
        /// <returns>
        /// <para /> - the Internal ID of the address (ad_numero)
        /// <para /> - the Manual Code of the address (ad_code)
        /// <para /> - the Name of the Address (ad_societe)
        /// <para /> - the operation performed since the date passed as parameter (operation)
        /// </returns>
        public async Task<Response<List<Address>>> Addresses(DateTime? dDateSince = null)
        {
            var parameters = new object[] { dDateSince?.ToOuinneBiseString() }.AsEnumerable().Where(p => p != null).ToArray();

            return await RequestAsync<Response<List<Address>>>(new BaseRequest(parameters));
        }

        /// <summary>
        /// This method can return various information related to an address.
        /// </summary>
        /// <param name="cMethod">Piece of information to be returned. The possible values for cInfo are in <see cref="AdInfoMethodsEnum"/></param>
        /// <param name="nAdresse">Only the transactions concerning the addresses with ad_numero = nAdresse are considered.</param>
        /// <param name="dDateEnd">The transactions are selected up to the date specified. The parameter is optional.
        /// If the parameter is missing all the transactions are selected.</param>
        /// <param name="dDateStart">The transactions are selected starting from the date specified. The parameter is optional.
        /// If the parameter is missing all the transactions are selected</param>
        /// <param name="vStock">This parameter is used only if cInfo is customersalesitem or supplierpurchasesitem.
        /// If the type of vStock is a string, the cInfo is applied to the Items being in the group specified in vStock.</param>
        /// <returns></returns>
        public async Task<Response<decimal>> AdInfo(AdInfoMethodsEnum cMethod, int nAdresse, DateTime? dDateEnd = null, DateTime? dDateStart = null, string vStock = null)
        {
            var parameters = new object[] { cMethod.ToDescriptionString(), nAdresse, dDateEnd?.ToOuinneBiseString(), dDateStart?.ToOuinneBiseString(), vStock }.AsEnumerable().Where(p => p != null).ToArray();

            return await RequestAsync<Response<decimal>>(new BaseRequest(parameters));
        }

        /// <summary>
        /// <see cref="AdInfo(AdInfoMethodsEnum,int,DateTime?,DateTime?,string)"/>
        /// </summary>
        /// <param name="method">Piece of information to be returned. The possible values for cInfo are in <see cref="AdInfoMethodsEnum"/></param>
        /// <param name="nAdresse">Only the transactions concerning the addresses with ad_numero = nAdresse are considered.</param>
        /// <param name="vStock">This parameter is used only if cInfo is customersalesitem or supplierpurchasesitem.
        /// If the type of vStock is a numeric, the cInfo is applied to the Item with ar_numero = vStock.</param>
        /// <param name="dDateEnd">The transactions are selected up to the date specified. The parameter is optional.
        /// If the parameter is missing all the transactions are selected.</param>
        /// <param name="dDateStart">The transactions are selected starting from the date specified. The parameter is optional.
        /// If the parameter is missing all the transactions are selected</param>
        /// <returns></returns>
        public async Task<Response<int>> AdInfo(AdInfoMethodsEnum method, int nAdresse, int vStock, DateTime? dDateEnd = null, DateTime? dDateStart = null)
        {
            var parameters = new object[] { method.ToDescriptionString(), nAdresse, dDateEnd?.ToOuinneBiseString(), dDateStart?.ToOuinneBiseString(), vStock }.AsEnumerable().Where(p => p != null).ToArray();

            return await RequestAsync<Response<int>>(new BaseRequest(parameters));
        }

        /// <summary>
        /// This method returns a piece of information from a given commercial document.
        /// </summary>
        /// <param name="method">Piece of information to be returned. The possible values for cInfo are in <see cref="DocInfoMethodsEnum"/></param>
        /// <param name="dEndDate">The transactions are selected up to the date specified. The parameter is optional.
        /// If the parameter is missing all the transactions are selected.</param>
        /// <param name="dStartDate">The transactions are selected starting from the date specified. The parameter is optional.
        /// If the parameter is missing all the transactions are selected</param>
        /// <param name="nComplement">Parameter that can be coupled with some of the cMethod parameter.
        /// Parameter that can be coupled with some of the cMethod parameter.
        /// <para /> If cMethod = vente chiffre affaire paiement méthode then it can contain the ID of the payment method of the documents (do_ban_pay).
        /// <para /> If cMethod = vente chiffre affaire paiement type then it can contain the ID of the type of the payment method of the documents (bq_type).
        /// <para /> If cMethod = vente chiffre affaire categorie then it can contain the ID of the category of the documents (do_catego).
        /// <para /> If cMethod = achat montant total categorie then it must contain the ID of the category of the documents (do_catego).</param>
        /// <param name="cComplement">Parameter that can be coupled with some of the cMethod parameter.
        /// <para /> If cMethod = vente chiffre affaire groupe article then it can contain the items group on which a filter has to be applied.
        /// <para /> If cMethod = vente marge groupe then it can contain the items group on which a filter has to be applied.
        /// <para /> If cMethod = achat montant total groupe article then it can contain the items group on which a filter has to be applied.</param>
        /// <returns></returns>
        public async Task<Response<decimal>> DocInfo(DocInfoMethodsEnum method, DateTime dEndDate, DateTime dStartDate, int? nComplement = null, string cComplement = null)
        {
            var parameters = new object[] { method.ToDescriptionString(), dEndDate.ToOuinneBiseString(), dStartDate.ToOuinneBiseString(), nComplement, cComplement }.AsEnumerable().Where(p => p != null).ToArray();

            return await RequestAsync<Response<decimal>>(new BaseRequest(parameters));
        }

        /// <summary>
        /// This method returns the number of the open (unpaid) documents along with the open (unpaid) amount in the local currency.
        /// The results grouped by document type and payment delay. Up to three different delays can be passed as a parameter.
        /// </summary>
        /// <param name="nFirstIntervalLimit">This parameter allows to specify a first group of late payments.
        /// The method will return the documents for which the payments are late, up to the number of days specified in the parameter.
        /// The delay is calculated from the due date. If documents with multiple due dates exist, each due date is taken into consideration.</param>
        /// <param name="nSecondIntervalLimit">This parameter allows to specify a first group of late payments.
        /// The interval taken into consideration is the one between nFirstIntervalLimit+1 and the number of days passed as parameter here.</param>
        /// <param name="nThirdIntervalLimit">Third limit to set up the delay intervals.
        /// The method returns all open documents with a delay between the second and third interval limit, and all open documents with a delay greater than this interval limit.</param>
        /// <returns> Return all pending payments for the intervals
        /// <para /> Remark: The method will also always return the payments that have a delay bigger than the bigger delay specified in the parameters</returns>
        public async Task<Response<object>> PendingPayments(int nFirstIntervalLimit, int? nSecondIntervalLimit = null, int? nThirdIntervalLimit = null)
        {
            var parameters = new object[] { nFirstIntervalLimit, nSecondIntervalLimit, nThirdIntervalLimit }.AsEnumerable().Where(p => p != null).ToArray();

            return await RequestAsync<Response<object>>(new BaseRequest(parameters));
        }

            return res;
        }

        /// <summary>
        /// This method returns the folders list.
        /// The Headers "winbiz-companyid" and "winbiz-year" are compulsory, but can be left empty when this method is used.
        /// The method can be used as an alternative way to prompt the user with a list of folders / fiscal year that replaces the need to enter manually this values at the logon.
        /// </summary>
        /// <returns>Returns the complete list of the folders that are available in the user's Winbiz Cloud, no matter the content of the "winbiz-companyid" and "winbiz-year" headers.</returns>
        public async Task<Response<List<Dossier>>> Folders() => await RequestAsync<Response<List<Dossier>>>(new BaseRequest());

        private async Task<T> RequestAsync<T>(BaseRequest request) where T : IBaseResponse
        {
            try
            {
                var result = await _apiService.Req<T>(request,
                                                _ouinneBiseApiSettings.Company,
                                                _ouinneBiseApiSettings.Username,
                                                _ouinneBiseApiSettings.Password.Encrypt(_ouinneBiseApiSettings.EncryptionKey),
                                                _companyId, _year, _ouinneBiseApiSettings.Key).ConfigureAwait(false);

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
                    case 111:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = $"An error occurred in the application {_appName}";
                        break;
                    //case 134:
                    case 197:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 250:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 280:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    //case 297:
                    case 299:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 327:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 420:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    //case 514:
                    case 535:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 666:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 667:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 668:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 672:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 673:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 689:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 717:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 737:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 837:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 864:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 905:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    case 999:
                        result.ErrorLevel = ErrorLevelEnum.WinBiz;
                        result.UserErrorMsg = "An error occurred in WinBIZ Cloud";
                        break;
                    default:
                        break;
                }

                return result;
            }
            catch (Exception e)
            {
                throw new OuinneBiseSharpException("La requête a échoué", e);
            }
        }
    }
}