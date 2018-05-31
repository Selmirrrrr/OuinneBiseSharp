namespace Bizy.OuinneBiseSharp.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Enums;
    using Extensions;
    using Services;
    using Xunit;

    public class ApiServiceTests
    {
        public ApiServiceTests()
        {
            _service = new OuinneBiseSharpService(Environment.GetEnvironmentVariable("WINBIZ_API_COMPANY"), Environment.GetEnvironmentVariable("WINBIZ_API_USERNAME"),
                Environment.GetEnvironmentVariable("WINBIZ_API_PASSWORD").Encrypt(), WinBizCompanyId, WinBizYear, Environment.GetEnvironmentVariable("WINBIZ_API_KEY"), "BizyBoard");
        }

        private readonly OuinneBiseSharpService _service;
        private const int WinBizCompanyId = 2;
        private const int WinBizYear = 2018;

        [Fact]
        public async Task AdInfo_CustomerBalanceMethod_ReturnsValue()
        {
            var response = await _service.AdInfo(AdInfoMethodsEnum.CustomerBalance, 18).ConfigureAwait(false);

            Assert.True(response.ErrorsCount == 0);
        }

        [Fact]
        public async Task AdInfo_CustomerInvoicesOpen_ReturnsValue()
        {
            var response = await _service.AdInfo(AdInfoMethodsEnum.CustomerInvoicesOpen, 18).ConfigureAwait(false);

            Assert.True(response.ErrorsCount == 0);
        }

        [Fact]
        public async Task AdInfo_CustomerSalesItem_ReturnsValue()
        {
            var response = await _service.AdInfo(AdInfoMethodsEnum.CustomerSalesItem, 18, vStock: "SERVICES").ConfigureAwait(false);

            Assert.True(response.ErrorsCount == 0);
        }

        [Fact]
        public async Task AdInfo_SalesCount_ReturnsValue()
        {
            var response = await _service.AdInfo(AdInfoMethodsEnum.SalesCount, 18).ConfigureAwait(false);

            Assert.True(response.ErrorsCount == 0);
        }

        [Fact]
        public async Task Adresses_ReturnsAddressesList_WhenNoLimitDateIsSet()
        {
            var response = await _service.Addresses().ConfigureAwait(false);

            Assert.True(response.Value.Any());
        }

        [Fact]
        public async Task DocInfo_VenteChiffreAffaire_ReturnsValue()
        {
            var response = await _service.DocInfo(DocInfoMethodsEnum.VenteChiffreAffaire, new DateTime(DateTime.Now.Year, 1, 31), new DateTime(DateTime.Now.Year, 1, 1)).ConfigureAwait(false);

            Assert.True(response.ErrorsCount == 0);
        }

        [Fact]
        public async Task Folders_ReturnsValue()
        {
            var tempService = new OuinneBiseSharpService(Environment.GetEnvironmentVariable("WINBIZ_API_COMPANY"), Environment.GetEnvironmentVariable("WINBIZ_API_USERNAME"),
                Environment.GetEnvironmentVariable("WINBIZ_API_PASSWORD").Encrypt(), 0, 0, Environment.GetEnvironmentVariable("WINBIZ_API_KEY"), "BizyBoard");
            var folders = await tempService.Folders();
            Assert.True(folders.Value.Count > 1);
        }

        [Fact]
        public async Task Stock_ReturnsStock_WhenProductExists()
        {
            var response = await _service.Stock(StockMethodsEnum.Available, 108).ConfigureAwait(false);

            Assert.True(response.ErrorsCount == 0);
        }

        [Fact]
        public async Task PendingPayments_ReturnsPendingPayaments()
        {
            var response = await _service.PendingPayments(30);

            Assert.True(response.Value.Any());
        }

        [Fact]
        public async Task AddressesPendingPayments_ReturnsPendingPayamentsByAddresses()
        {
            var response = await _service.AddressesPendingPayments(30);
            Assert.True(response.ErrorsCount == 0);
        }

        [Fact]
        public async Task AddressPendingPayments_ReturnsPendingPayamentsForAnAddresses()
        {
            var response = await _service.AddressPendingPayments(18, 30);
            Assert.True(response.ErrorsCount == 0);
        }

        [Fact]
        public async Task PaymentsCalendar_ReturnsDuePayments()
        {
            var response = await _service.PaymentsCalendar(9999);
            Assert.True(response.ErrorsCount == 0);
        }

        [Fact]
        public async Task AddressesPaymentsCalendar_ReturnsPendingCalendarByAddresses()
        {
            var response = await _service.AddressesPendingPayments(9999);
            Assert.True(response.ErrorsCount == 0);
        }
    }
}