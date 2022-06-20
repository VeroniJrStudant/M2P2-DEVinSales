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
        [TestCase(1, 1, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(-3, 14, 11)]
        public void DeveSomarCorretamente(int x, int y, int expected)
        {
            var result = CalculatorService.Soma(x, y);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(2, 2, 4)]
        [TestCase(5, 5, 25)]
        [TestCase(9, 9, 81)]
        public void DeveMultiplicarCorretamente(int x, int y, int expected)
        {
            var result = CalculatorService.Multiplicar(x, y);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(3, 2, 1)]
        [TestCase(10, 5, 5)]
        [TestCase(109, 9, 100)]
        public void DeveSubtrairCorretamente(int x, int y, int expected)
        {
            var result = CalculatorService.Subtrair(x, y);
            Assert.AreEqual(expected, result);
        }
    }
}