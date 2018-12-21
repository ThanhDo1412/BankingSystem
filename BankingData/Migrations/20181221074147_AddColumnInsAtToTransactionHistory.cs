using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingData.Migrations
{
    public partial class AddColumnInsAtToTransactionHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsAt",
                table: "TransactionHistories",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsAt",
                table: "TransactionHistories");
        }
    }
}
