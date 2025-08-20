using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TptUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionStatus",
                table: "Transactions");

            migrationBuilder.CreateTable(
                name: "TransferTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TransferToAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferTransactions_Transactions_Id",
                        column: x => x.Id,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferTransactions");

            migrationBuilder.AddColumn<int>(
                name: "TransactionStatus",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
