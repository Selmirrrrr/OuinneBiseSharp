namespace Bizy.OuinneBiseSharp.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Enums;
    using Microsoft.Extensions.Logging;
    using Services;
    using Xunit;

    public class ApiServiceTests
    {
        private readonly OuinneBiseSharpService _service;

        public ApiServiceTests()
        {
            var settings = new OuinneBiseApiSettings(Environment.GetEnvironmentVariable("WINBIZ_API_KEY"),
                Environment.GetEnvironmentVariable("WINBIZ_API_COMPANY"),
                Environment.GetEnvironmentVariable("WINBIZ_API_USERNAME"),
                Environment.GetEnvironmentVariable("WINBIZ_API_PASSWORD"),
                "BgIAAACkAABSU0ExAAQAAAEAAQBZ3myd6ZQA0tUXZ3gIzu1sQ7larRfM5KFiYbkgWk+jw2VEWpxpNNfDw8M3MIIbbDeUG02y/ZW+XFqyMA/87kiGt9eqd9Q2q3rRgl3nWoVfDnRAPR4oENfdXiq5oLW3VmSKtcBl2KzBCi/J6bbaKmtoLlnvYMfDWzkE3O1mZrouzA==",
                "https://api.winbizcloud.ch/");
            _service = new OuinneBiseSharpService(settings, "BizyBoard", 2, 2018, new Logger<OuinneBiseSharpService>(new LoggerFactory()));
        }

        [Fact]
        public async Task GetStock_ReturnsStock_WhenProductExists()
        {
            var response = await _service.Stock(108).ConfigureAwait(false);

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

            Assert.True(response.Value == 240565);
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

            Assert.True(response.Value == 021509M);
        }

        [Fact]
        public async Task Folders_ReturnsValue()
        {
            try
            {
                var folders = await _service.Folders();
                Assert.True(folders.Value.Count > 1);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}