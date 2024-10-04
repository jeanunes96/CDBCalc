using CDBCalc.Core.Service.Interface;
using CDBCalc.Domain.Model;
using CDBCalc.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace CDBCalc.Test.ControllerTests
{
    [TestFixture]
    public class InvestmentControllerTests
    {
        private Mock<IInvestmentCalculatorService> _mockCalculatorService;
        private InvestmentController _controller;

        [SetUp]
        public void Setup()
        {
            _mockCalculatorService = new Mock<IInvestmentCalculatorService>();
            _controller = new InvestmentController(_mockCalculatorService.Object);
        }

        [Test]
        public void CalculateCdb_ValidRequest_ReturnsOkResult()
        {
            var request = new CalculationRequest
            {
                InitialValue = 1000m,
                TotalMonth = 12
            };

            var expectedResponse = new CalculationResponse
            {
                GrossAmount = 1100m,
                NetAmount = 1000m,
                TaxAmount = 100m,
                InitialValue = 1000m,
                Cdi = 0.009m,
                BankRate = 1.08m,
                TotalMonths = 12
            };

            _mockCalculatorService.Setup(service => service.CalculateCdb(request)).Returns(expectedResponse);

            var result = _controller.CalculateCdb(request);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(okResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
                Assert.That(okResult.Value, Is.EqualTo(expectedResponse));
            });
        }

        [Test]
        public void CalculateCdb_NullRequest_ReturnsBadRequest()
        {
            var result = _controller.CalculateCdb(null);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result.Result as BadRequestObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(badRequestResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
                Assert.That(badRequestResult.Value, Is.EqualTo("Request cannot be null."));
            });
        }
    }
}
