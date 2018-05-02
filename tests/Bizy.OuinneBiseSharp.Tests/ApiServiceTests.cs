namespace Bizy.OuinneBiseSharp.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Enums;
    using Services;
    using Xunit;

    public class ApiServiceTests
    {
        private readonly OuinneBiseSharpService _service;
        private readonly int _winBizCompanyId = 2;
        private readonly int _winBizYear = 2018;

        public ApiServiceTests()
        {
            _service = new OuinneBiseSharpService(Environment.GetEnvironmentVariable("WINBIZ_API_COMPANY"), Environment.GetEnvironmentVariable("WINBIZ_API_USERNAME"),
                Environment.GetEnvironmentVariable("WINBIZ_API_PASSWORD"), _winBizCompanyId, _winBizYear, Environment.GetEnvironmentVariable("WINBIZ_API_KEY"), "BizyBoard");
        }

        [Fact]
        public async Task Stock_ReturnsStock_WhenProductExists()
        {
            var response = await _service.Stock(StockMethodsEnum.Available, 108).ConfigureAwait(false);

            Assert.True(response.Value == 100);
        }

        [Fact]
        public async Task Adresses_ReturnsAddressesList_WhenNoLimitDateIsSet()
        {
            var response = await _service.Addresses().ConfigureAwait(false);

            Assert.True(response.Value.Any());
        }

        [Fact]
        public async Task AdInfo_CustomerBalanceMethod_ReturnsValue()
        {
            var response = await _service.AdInfo(AdInfoMethodsEnum.CustomerBalance, 18).ConfigureAwait(false);

            Assert.True(response.Value == 2405.65M);
        }

        [Fact]
        public async Task AdInfo_CustomerInvoicesOpen_ReturnsValue()
        {
            var response = await _service.AdInfo(AdInfoMethodsEnum.CustomerInvoicesOpen, 18).ConfigureAwait(false);

            Assert.True(response.Value == 4);
        }

        [Fact]
        public async Task AdInfo_SalesCount_ReturnsValue()
        {
            var response = await _service.AdInfo(AdInfoMethodsEnum.SalesCount, 18).ConfigureAwait(false);

            Assert.True(response.Value == 4);
        }

        [Fact]
        public async Task AdInfo_CustomerSalesItem_ReturnsValue()
        {
            var response = await _service.AdInfo(AdInfoMethodsEnum.CustomerSalesItem, 18, vStock: "SERVICES").ConfigureAwait(false);

            Assert.True(response.Value == 2150.9M);
        }

        [Fact]
        public async Task DocInfo_VenteChiffreAffaire_ReturnsValue()
        {
            var response = await _service.DocInfo(DocInfoMethodsEnum.VenteChiffreAffaire, new DateTime(DateTime.Now.Year, 1, 31), new DateTime(DateTime.Now.Year, 1, 1)).ConfigureAwait(false);

            Assert.True(response.Value == 5284.04M);
        }

        [Fact]
        public async Task Folders_ReturnsValue()
        {
            var folders = await _service.Folders();
            Assert.True(folders.Value.Count > 1);
        }
    }
}