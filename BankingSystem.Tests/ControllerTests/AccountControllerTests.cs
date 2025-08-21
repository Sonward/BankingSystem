using BankingSystem.DTO;
using BankingSystem.DTO.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private BankingSystemTestFixute _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new BankingSystemTestFixute();
        }

        [TearDown]
        public void TearDown()
        {
            _fixture?.Dispose();
        }

        [Test]
        public async Task GetAllAsync_ReturnsOkWithEmptyList_WhenNoAccountsExist()
        {
            // Act
            var result = await _fixture.AccountController.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var accounts = okResult.Value as IEnumerable<AccountDTO>;
            Assert.That(accounts, Is.Not.Null);
            Assert.That(accounts.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllAsync_ReturnsOkWithAccounts_WhenAccountsExist()
        {
            // Arrange
            var createRequest = new AccountCreateRequest("John Doe", 1000m);
            await _fixture.AccountController.CreateAsync(createRequest);

            // Act
            var result = await _fixture.AccountController.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var accounts = okResult.Value as IEnumerable<AccountDTO>;
            Assert.That(accounts, Is.Not.Null);
            Assert.That(accounts.Count(), Is.EqualTo(1));

            var firstAccount = accounts.First();
            Assert.That(firstAccount.OwnerName, Is.EqualTo("John Doe"));
            Assert.That(firstAccount.Balance, Is.EqualTo(1000m));
        }

        [Test]
        public async Task GetByAccountNumberAsync_ReturnsBadRequest_WhenAccountNumberIsNull()
        {
            // Act
            var result = await _fixture.AccountController.GetByAccountNumberAsync(null);

            // Assert
            Assert.That(result, Is.Not.Null);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("accountNumber is required."));
        }

        [Test]
        public async Task GetByAccountNumberAsync_ReturnsBadRequest_WhenAccountNumberIsEmpty()
        {
            // Act
            var result = await _fixture.AccountController.GetByAccountNumberAsync("");

            // Assert
            Assert.That(result, Is.Not.Null);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("accountNumber is required."));
        }

        [Test]
        public async Task GetByAccountNumberAsync_ReturnsBadRequest_WhenAccountNumberIsWhitespace()
        {
            // Act
            var result = await _fixture.AccountController.GetByAccountNumberAsync("   ");

            // Assert
            Assert.That(result, Is.Not.Null);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("accountNumber is required."));
        }

        [Test]
        public async Task GetByAccountNumberAsync_ReturnsNotFound_WhenAccountDoesNotExist()
        {
            // Act
            var result = await _fixture.AccountController.GetByAccountNumberAsync("123456789");

            // Assert
            Assert.That(result, Is.Not.Null);
            var notFoundResult = result.Result as NotFoundResult;
            Assert.That(notFoundResult, Is.Not.Null);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task GetByAccountNumberAsync_ReturnsOkWithAccount_WhenAccountExists()
        {
            // Arrange
            var createRequest = new AccountCreateRequest("Jane Smith", 2000m);
            var createResult = await _fixture.AccountController.CreateAsync(createRequest);
            var createdAccount = ((OkObjectResult)createResult.Result).Value as AccountDTO;

            // Act
            var result = await _fixture.AccountController.GetByAccountNumberAsync(createdAccount.Number);

            // Assert
            Assert.That(result, Is.Not.Null);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var account = okResult.Value as AccountDTO;
            Assert.That(account, Is.Not.Null);
            Assert.That(account.Id, Is.EqualTo(createdAccount.Id));
            Assert.That(account.Number, Is.EqualTo(createdAccount.Number));
            Assert.That(account.OwnerName, Is.EqualTo("Jane Smith"));
            Assert.That(account.Balance, Is.EqualTo(2000m));
        }

        [Test]
        public async Task CreateAsync_ReturnsOkWithCreatedAccount_WhenValidRequest()
        {
            // Arrange
            var request = new AccountCreateRequest("Bob Johnson", 5000m);

            // Act
            var result = await _fixture.AccountController.CreateAsync(request);

            // Assert
            Assert.That(result, Is.Not.Null);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var account = okResult.Value as AccountDTO;
            Assert.That(account, Is.Not.Null);
            Assert.That(account.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(account.Number, Is.Not.Null.And.Not.Empty);
            Assert.That(account.OwnerName, Is.EqualTo("Bob Johnson"));
            Assert.That(account.Balance, Is.EqualTo(5000m));
            Assert.That(account.CreationTime, Is.LessThanOrEqualTo(DateTime.UtcNow));
        }

        [Test]
        public async Task CreateAsync_CreatesMultipleAccounts_WithUniqueNumbers()
        {
            // Arrange
            var request1 = new AccountCreateRequest("Alice Brown", 1500m);
            var request2 = new AccountCreateRequest("Charlie Davis", 3000m);

            // Act
            var result1 = await _fixture.AccountController.CreateAsync(request1);
            var result2 = await _fixture.AccountController.CreateAsync(request2);

            // Assert
            var account1 = ((OkObjectResult)result1.Result).Value as AccountDTO;
            var account2 = ((OkObjectResult)result2.Result).Value as AccountDTO;

            Assert.That(account1, Is.Not.Null);
            Assert.That(account2, Is.Not.Null);
            Assert.That(account1.Id, Is.Not.EqualTo(account2.Id));
            Assert.That(account1.Number, Is.Not.EqualTo(account2.Number));
            Assert.That(account1.OwnerName, Is.EqualTo("Alice Brown"));
            Assert.That(account2.OwnerName, Is.EqualTo("Charlie Davis"));
        }

        [Test]
        public async Task CreateAsync_WithZeroBalance_CreatesAccountSuccessfully()
        {
            // Arrange
            var request = new AccountCreateRequest("Zero Balance User", 0m);

            // Act
            var result = await _fixture.AccountController.CreateAsync(request);

            // Assert
            Assert.That(result, Is.Not.Null);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            var account = okResult.Value as AccountDTO;
            Assert.That(account, Is.Not.Null);
            Assert.That(account.Balance, Is.EqualTo(0m));
            Assert.That(account.OwnerName, Is.EqualTo("Zero Balance User"));
        }

        [Test]
        public async Task Integration_CreateThenRetrieveAccount_WorksCorrectly()
        {
            // Arrange
            var createRequest = new AccountCreateRequest("Integration Test User", 7500m);

            // Act - Create account
            var createResult = await _fixture.AccountController.CreateAsync(createRequest);
            var createdAccount = ((OkObjectResult)createResult.Result).Value as AccountDTO;

            // Act - Retrieve by number
            var getResult = await _fixture.AccountController.GetByAccountNumberAsync(createdAccount.Number);
            var retrievedAccount = ((OkObjectResult)getResult.Result).Value as AccountDTO;

            // Act - Get all accounts
            var getAllResult = await _fixture.AccountController.GetAllAsync();
            var allAccounts = ((OkObjectResult)getAllResult.Result).Value as IEnumerable<AccountDTO>;

            // Assert
            Assert.That(retrievedAccount, Is.Not.Null);
            Assert.That(retrievedAccount.Id, Is.EqualTo(createdAccount.Id));
            Assert.That(retrievedAccount.Number, Is.EqualTo(createdAccount.Number));
            Assert.That(retrievedAccount.OwnerName, Is.EqualTo("Integration Test User"));
            Assert.That(retrievedAccount.Balance, Is.EqualTo(7500m));

            Assert.That(allAccounts, Is.Not.Null);
            Assert.That(allAccounts.Count(), Is.GreaterThanOrEqualTo(1));
            Assert.That(allAccounts.Any(a => a.Id == createdAccount.Id), Is.True);
        }
    }
}