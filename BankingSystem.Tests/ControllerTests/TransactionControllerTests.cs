using BankingSystem.Controllers;
using BankingSystem.DTO.EntityDTO;
using BankingSystem.DTO.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Tests.ControllerTests;

[TestFixture]
public class TransactionControllerTests
{
    private BankingSystemTestFixute _fixture;
    private TransactionController _controller;

    [SetUp]
    public void SetUp()
    {
        _fixture = new BankingSystemTestFixute();
        _controller = _fixture.TransactionController;
    }

    [TearDown]
    public void TearDown()
    {
        _fixture?.Dispose();
    }

    [Test]
    public async Task Deposit_WithValidRequest_ReturnsOkWithTransactionDTO()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, 500m);

        // Act
        var result = await _controller.Deposit(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<TransactionDTO>());

        var transaction = okResult.Value as TransactionDTO;
        Assert.That(transaction.Amount, Is.EqualTo(500m));
        Assert.That(transaction.TargetAccountNumber, Is.EqualTo(targetAccount.Number));
    }

    [Test]
    public async Task Deposit_WithZeroAmount_CallsServiceWithZeroAmount()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, 0m);

        // Act
        var result = await _controller.Deposit(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransactionDTO;
        Assert.That(transaction.Amount, Is.EqualTo(0m));
    }

    [Test]
    public async Task Deposit_WithNegativeAmount_CallsServiceWithNegativeAmount()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, -100m);

        // Act
        var result = await _controller.Deposit(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransactionDTO;
        Assert.That(transaction.Amount, Is.EqualTo(-100m));
    }

    [Test]
    public async Task Withdraw_WithValidRequest_ReturnsOkWithTransactionDTO()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, 300m);

        // Act
        var result = await _controller.Withdraw(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<TransactionDTO>());

        var transaction = okResult.Value as TransactionDTO;
        Assert.That(transaction.Amount, Is.EqualTo(300m));
        Assert.That(transaction.TargetAccountNumber, Is.EqualTo(targetAccount.Number));
    }

    [Test]
    public async Task Withdraw_WithZeroAmount_CallsServiceWithZeroAmount()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, 0m);

        // Act
        var result = await _controller.Withdraw(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransactionDTO;
        Assert.That(transaction.Amount, Is.EqualTo(0m));
    }

    [Test]
    public async Task Withdraw_WithLargeAmount_CallsServiceWithLargeAmount()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, 2000m);

        // Act
        var result = await _controller.Withdraw(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.That(badRequestResult.Value, Is.Not.Null);
        var errorResponse = badRequestResult.Value;
        Assert.That(errorResponse.ToString(), Does.Contain("message"));
    }

    [Test]
    public async Task Transfer_WithValidRequest_ReturnsOkWithTransferTransactionDTO()
    {
        // Arrange
        var sourceAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("Jane Smith", 500m));
        var request = new TransferTransactionCreateRequest(sourceAccount, targetAccount, 250m);

        // Act
        var result = await _controller.Transfer(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<TransferTransactionDTO>());

        var transaction = okResult.Value as TransferTransactionDTO;
        Assert.That(transaction.Amount, Is.EqualTo(250m));
        Assert.That(transaction.TargetAccountNumber, Is.EqualTo(sourceAccount.Number));
        Assert.That(transaction.TransferToAccountNumber, Is.EqualTo(targetAccount.Number));
    }

    [Test]
    public async Task Transfer_WithZeroAmount_CallsServiceWithZeroAmount()
    {
        // Arrange
        var sourceAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("Jane Smith", 500m));
        var request = new TransferTransactionCreateRequest(targetAccount, sourceAccount, 0m);

        // Act
        var result = await _controller.Transfer(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransferTransactionDTO;
        Assert.That(transaction.Amount, Is.EqualTo(0m));
    }

    [Test]
    public async Task Transfer_WithSameSourceAndTarget_CallsService()
    {
        // Arrange
        var account = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransferTransactionCreateRequest(account, account, 100m);

        // Act
        var result = await _controller.Transfer(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransferTransactionDTO;
        Assert.That(transaction.TargetAccountNumber, Is.EqualTo(account.Number));
        Assert.That(transaction.TransferToAccountNumber, Is.EqualTo(account.Number));
    }

    [Test]
    public async Task Deposit_ReturnsTransactionWithCorrectType()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, 500m);

        // Act
        var result = await _controller.Deposit(request);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransactionDTO;
        Assert.That(transaction.TransactionType, Is.EqualTo(TransactionType.Deposit));
    }

    [Test]
    public async Task Withdraw_ReturnsTransactionWithCorrectType()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, 300m);

        // Act
        var result = await _controller.Withdraw(request);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransactionDTO;
        Assert.That(transaction.TransactionType, Is.EqualTo(TransactionType.Withdraw));
    }

    [Test]
    public async Task Transfer_ReturnsTransactionWithCorrectType()
    {
        // Arrange
        var sourceAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("Jane Smith", 500m));
        var request = new TransferTransactionCreateRequest(sourceAccount, targetAccount, 250m);

        // Act
        var result = await _controller.Transfer(request);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransferTransactionDTO;
        Assert.That(transaction.TransactionType, Is.EqualTo(TransactionType.Transfer));
    }

    [Test]
    public async Task Deposit_ReturnsTransactionWithValidId()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, 500m);

        // Act
        var result = await _controller.Deposit(request);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransactionDTO;
        Assert.That(transaction.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public async Task Withdraw_ReturnsTransactionWithValidId()
    {
        // Arrange
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var request = new TransactionCreateRequest(targetAccount, 300m);

        // Act
        var result = await _controller.Withdraw(request);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransactionDTO;
        Assert.That(transaction.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public async Task Transfer_ReturnsTransactionWithValidId()
    {
        // Arrange
        var sourceAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("John Doe", 1000m));
        var targetAccount = await _fixture.AccountService.CreateAccountAsync(new AccountCreateRequest("Jane Smith", 500m));
        var request = new TransferTransactionCreateRequest(sourceAccount, targetAccount, 250m);

        // Act
        var result = await _controller.Transfer(request);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var transaction = okResult.Value as TransferTransactionDTO;
        Assert.That(transaction.Id, Is.Not.EqualTo(Guid.Empty));
    }
}