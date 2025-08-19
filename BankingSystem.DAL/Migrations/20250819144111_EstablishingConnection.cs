using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EstablishingConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DindedAccountId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DindedAccountId",
                table: "Transactions",
                column: "DindedAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_DindedAccountId",
                table: "Transactions",
                column: "DindedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_DindedAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DindedAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DindedAccountId",
                table: "Transactions");
        }
    }
}
