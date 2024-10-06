using CDBCalc.Core.Service;
using CDBCalc.Core.Service.Interface;
using CDBCalc.Domain.Model;
using NUnit.Framework;

namespace CDBCalc.Tests.Services
{
    [TestFixture]
    public class InvestmentCalculatorServiceTests
    {
        private IInvestmentCalculatorService _investmentCalculatorService;

        [SetUp]
        public void Setup()
        {
            _investmentCalculatorService = new InvestmentCalculatorService();
        }

        [Test]
        public void CalculateCdb_ValidRequest_ReturnsCorrectResponse()
        {
            var request = new CalculationRequest
            {
                InitialValue = 1000m,
                TotalMonth = 12
            };

            var response = _investmentCalculatorService.CalculateCdb(request);
          
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(response.GrossAmount, Is.GreaterThan(0));
                Assert.That(response.NetAmount, Is.GreaterThan(0));
                Assert.That(response.TaxAmount, Is.GreaterThan(0));
                Assert.That(response.InitialValue, Is.EqualTo(request.InitialValue));
                Assert.That(response.TotalMonths, Is.EqualTo(request.TotalMonth));
                Assert.That(response.Cdi, Is.EqualTo(0.009m));
                Assert.That(response.BankRate, Is.EqualTo(1.08m));
            });
        }

        [Test]
        public void CalculateCdb_ZeroInitialValue_ReturnsCorrectResponse()
        {
            var request = new CalculationRequest
            {
                InitialValue = 0m,
                TotalMonth = 12
            };

            var response = _investmentCalculatorService.CalculateCdb(request);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(response.GrossAmount, Is.EqualTo(0));
                Assert.That(response.NetAmount, Is.EqualTo(0));
                Assert.That(response.TaxAmount, Is.EqualTo(0));
                Assert.That(response.InitialValue, Is.EqualTo(request.InitialValue));
                Assert.That(response.TotalMonths, Is.EqualTo(request.TotalMonth));
            });
        }

        [Test]
        public void CalculateCdb_NegativeEarnings_ReturnsCorrectTax()
        {
            var request = new CalculationRequest
            {
                InitialValue = 1000m,
                TotalMonth = 12
            };

            var response = _investmentCalculatorService.CalculateCdb(request);

            Assert.That(response.TaxAmount, Is.GreaterThanOrEqualTo(0));
        }
    }
}
