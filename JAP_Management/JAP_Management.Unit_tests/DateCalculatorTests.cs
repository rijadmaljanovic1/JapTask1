using JAP_Management.Core.Helpers;
//using NUnit.Framework;

namespace JAP_Management.Unit_tests
{
    public class DateCalculatorTests
    {
        [Fact]
        public void PositiveTestCase1()
        {
            DateTime date =new DateTime(2022, 10, 27, 0, 0, 0);
            int duration = 16;
            var newDate =DateCalculator.CalculateEndDate(date, duration);
            Assert.True(newDate == new DateTime(2022, 10, 28, 0, 0, 0));
        }
        [Fact]
        public void PositiveTestCase2()
        {
            DateTime date = new DateTime(2022, 10, 27, 0, 0, 0);
            int duration = 25;
            var newDate = DateCalculator.CalculateEndDate(date, duration);
            Assert.True(newDate == new DateTime(2022, 11, 1, 0, 0, 0));
        }
        [Fact]
        public void NegativeTestCase1()
        {
            DateTime date = new DateTime(2022, 10, 27, 0, 0, 0);
            int duration = 16;
            var newDate = DateCalculator.CalculateEndDate(date, duration);
            Assert.False(newDate == new DateTime(2022, 10, 29, 0, 0, 0));
        }
        [Fact]
        public void NegativeTestCase2()
        {
            DateTime date = new DateTime(2022, 10, 27, 0, 0, 0);
            int duration = 16;
            var newDate = DateCalculator.CalculateEndDate(date, duration);
            Assert.False(newDate == new DateTime(2022, 10, 31, 0, 0, 0));
        }
    }
}