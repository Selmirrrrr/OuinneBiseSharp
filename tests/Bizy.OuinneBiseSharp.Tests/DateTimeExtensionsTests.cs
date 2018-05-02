namespace Bizy.OuinneBiseSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Extensions;

    public class DateTimeExtensionsTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {new DateTime(2018, 1, 1), "2018-01-01"},
                new object[] {new DateTime(2018, 1, 13), "2018-01-13"},
                new object[] {new DateTime(2018, 11, 9), "2018-11-09"},
                new object[] {new DateTime(2018, 12, 31), "2018-12-31"}
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void ToOuinneBiseString_ShouldFormatDateCorrectly(DateTime date, string expected)
        {
            Assert.Equal(expected, date.ToOuinneBiseString());
        }
    }
}
