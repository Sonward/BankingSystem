using BankingSystem.DAL;
using BankingSystem.DAL.Repositories;
using BankingSystem.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.DAL.Extencions
{
    public static class DbContextServiceCollectionExtensions
    {
        public static void RegisterDbContext(this IServiceCollection serviceCollection, string connectionString)
            => serviceCollection.AddDbContext<AppDbContext>(db => db.UseSqlServer(connectionString));

        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAccountRepository, AccountRepository>();
            serviceCollection.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
