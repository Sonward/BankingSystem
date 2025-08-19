using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EstablishingConnection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_DindedAccountId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "DindedAccountId",
                table: "Transactions",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_DindedAccountId",
                table: "Transactions",
                newName: "IX_Transactions_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Transactions",
                newName: "DindedAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                newName: "IX_Transactions_DindedAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_DindedAccountId",
                table: "Transactions",
                column: "DindedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
