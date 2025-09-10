using BankingSystem.BLL.Services;
using BankingSystem.BLL.Services.Implementation;
using BankingSystem.Controllers;
using BankingSystem.DAL;
using BankingSystem.DAL.Repositories;
using BankingSystem.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Tests;

internal class BankingSystemTestFixute : IDisposable
{
    public AppDbContext DbContext { get; private set; }
    public IAccountRepository AccountRepository { get; private set; }
    public ITransactionRepository TransactionRepository { get; private set; }
    public IAccountService AccountService { get; private set; }
    public ITransactionService TransactionService { get; private set; }
    public AccountController AccountController { get; private set; }
    public TransactionController TransactionController { get; private set; }

    public BankingSystemTestFixute()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("BankingSystemTestDb").Options;

        DbContext = new AppDbContext(options);

        AccountRepository = new AccountRepository(DbContext);
        TransactionRepository = new TransactionRepository(DbContext);

        AccountService = new AccountService(AccountRepository);
        TransactionService = new TransactionService(AccountRepository, TransactionRepository);

        AccountController = new AccountController(AccountService);
        TransactionController = new TransactionController(TransactionService);
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}
