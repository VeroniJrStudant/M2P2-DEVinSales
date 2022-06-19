using DevInSales.Services;
using NUnit.Framework;

namespace DEVinSalesTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1,1,2)]
        [TestCase(2,2,4)]
        [TestCase(-3,14,11)]
        public void DeveSomarCorretamente(int x, int y, int expected)
        {
            var result = CalculatorService.Soma(x, y);
            Assert.AreEqual(expected, result);
        }
    }
}