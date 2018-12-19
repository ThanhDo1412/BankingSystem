using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingDatabase.Migrations
{
    public partial class AddConcurrencyForAccountDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "AccountDetails",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "AccountDetails");
        }
    }
}
