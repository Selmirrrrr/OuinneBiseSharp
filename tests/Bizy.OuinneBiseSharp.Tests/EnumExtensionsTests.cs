namespace Bizy.OuinneBiseSharp.Tests
{
    using System.ComponentModel;
    using Extensions;
    using Xunit;

    public class EnumExtensionsTests
    {
        private enum WithDescriptionEnum
        {
            [Description("test")]
            Test
        }

        private enum WithoutDescriptionEnum
        {
            Test
        }

        [Fact]
        public void ToDescriptionString_ShouldReturnDescription_WhenExisitng()
        {
            Assert.Equal("test", WithDescriptionEnum.Test.ToDescriptionString());
        }

        [Fact]
        public void ToDescriptionString_ShouldReturnEmptyString_WhenTheresNoDesc()
        {
            Assert.True(string.IsNullOrWhiteSpace(WithoutDescriptionEnum.Test.ToDescriptionString()));
        }
    }
}