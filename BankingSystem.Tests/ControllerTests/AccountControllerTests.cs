using BankingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Tests.Controllers
{
    [TestFixture]
    internal class AccountControllerTests
    {
        private BankingSystemTestFixute _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new BankingSystemTestFixute();
        }

        [TearDown]
        public void TearDown()
        {
            _fixture.Dispose();
        }

        [Test]
        public async Task CreateAsync_ValidRequest_ReturnsCreatedAccount()
        {
            // Arrange
            var request = new AccountCreateRequest("Diana", 1200);

            // Act
            var result = await _fixture.AccountController.CreateAsync(request);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var account = okResult.Value as AccountDTO;
            Assert.That(account, Is.Not.Null);
            Assert.That(account.OwnerName, Is.EqualTo("Diana"));
            Assert.That(account.Balance, Is.EqualTo(1200));
        }

        [Test]
        public async Task GetByAccountNumberAsync_EmptyNumber_ReturnsBadRequest()
        {
            // Act
            var result = await _fixture.AccountController.GetByAccountNumberAsync("");

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetByAccountNumberAsync_NotExisting_ReturnsNotFound()
        {
            // Act
            var result = await _fixture.AccountController.GetByAccountNumberAsync("NON_EXISTING");

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetByAccountNumberAsync_ValidNumber_ReturnsAccount()
        {
            // Arrange
            var created = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("Charlie", 750));

            // Act
            var result = await _fixture.AccountController.GetByAccountNumberAsync(created.Number);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var account = okResult.Value as AccountDTO;
            Assert.That(account, Is.Not.Null);
            Assert.That(account.Number, Is.EqualTo(created.Number));
        }
    }
}
