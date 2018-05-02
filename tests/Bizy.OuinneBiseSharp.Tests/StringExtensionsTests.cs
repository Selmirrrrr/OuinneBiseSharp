namespace Bizy.OuinneBiseSharp.Tests
{
    using System;
    using Xunit;
    using Extensions;

    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void Encrypt_ShouldThrow_WhenDataIsEmpty(string data)
        {
            Assert.Throws<ArgumentNullException>(() => data.Encrypt(""));
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void Encrypt_ShouldThrow_WhenKeyIsEmpty(string key)
        {
            Assert.Throws<ArgumentNullException>(() => "test".Encrypt(key));
        }

        [Theory]
        [InlineData("asdasd")]
        [InlineData("BgIAAACkAABSU0ExAAQAAAEAAQBZ3myd6ZQA0tUXZ3gIzu1sQ7larRfM5KFiYbkgWk+jw2VEWpxpNNfDw8M3MIIbbDeUG02y/ZW+XFqyMA/87kiGt9eqd9Q2q3rRgl3nWoVfDnRAPR4oENfdXiq5oLW3VmSKtcBl2KzBCi/J6bbaKmtoLlnvYMfDWzkE3O1mZrouzA=")] //1 char missing
        [InlineData("BgIAAACkAABSU0ExAAQAAAEAAQBZ3myd6ZQA0tUXZ3gIzu1sQ7larRfM5KFiYbkgWk+jw2VEWpxpNNfDw8M3MIIbbDeUG02y/ZW+XFqyMA/87kiGt9eqd9Q2q3rRgl3nWoVfDnRAPR4oENfdXiq5oLW3VmSKtcBl2KzBCi/J6bbaKmtoLlnvYMfDWzkE3O1mZrouzA==1")] //1 char extra
        public void Encrypt_ShouldThrow_WhenKeyIsNotBase64(string key)
        {
            Assert.Throws<FormatException>(() => "test".Encrypt(key));
        }

        [Fact]
        public void Encrypt_ShouldEncrypt_WhenKeyIsOk()
        {
            Assert.True(!string.IsNullOrWhiteSpace("test".Encrypt("BgIAAACkAABSU0ExAAQAAAEAAQBZ3myd6ZQA0tUXZ3gIzu1sQ7larRfM5KFiYbkgWk+jw2VEWpxpNNfDw8M3MIIbbDeUG02y/ZW+XFqyMA/87kiGt9eqd9Q2q3rRgl3nWoVfDnRAPR4oENfdXiq5oLW3VmSKtcBl2KzBCi/J6bbaKmtoLlnvYMfDWzkE3O1mZrouzA==")));
        }
    }
}
