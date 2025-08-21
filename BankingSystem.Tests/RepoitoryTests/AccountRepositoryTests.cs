using BankingSystem.DAL;
using BankingSystem.DAL.Entities;
using BankingSystem.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Tests.RepoitoryTests
{
    [TestFixture]
    internal class AccountRepositoryTests
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
        public async Task CreateAsync_ValidAccount_AddsAndReturnsEntity()
        {
            // Arrange
            var acc = new Account
            {
                Number = "ACC-1000",
                OwnerName = "Tester",
                Balance = 1500m
            };

            // Act
            var created = await _fixture.AccountRepository.CreateAsync(acc);

            // Assert
            Assert.IsNotNull(created);
            Assert.That(created.Number, Is.EqualTo("ACC-1000"));

            // double-check persisted
            var fromDb = await _fixture.DbContext.Accounts.FindAsync(created.Id);
            Assert.IsNotNull(fromDb);
            Assert.That(fromDb!.Number, Is.EqualTo("ACC-1000"));
        }

        [Test]
        public async Task GetAllAsync_InitiallyEmpty_ReturnsEmptyCollection()
        {
            // Arrange
            var created = await _fixture.AccountRepository.CreateAsync(new Account
            {
                Number = "ACC-3000",
                OwnerName = "ByNumber",
                Balance = 300m
            });

            // Act
            var all = await _fixture.AccountRepository.GetAllAsync();

            // Assert
            Assert.IsNotNull(all);
            Assert.That(all.Count, Is.EqualTo(1));
        }
    }
}
