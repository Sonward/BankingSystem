using BankingSystem.DAL.Extencions;
using BankingSystem.BLL.Services;
using BankingSystem.BLL.Services.Implementation;
using BankingSystem.Common.Extencions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddRepositories();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseErrorHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
