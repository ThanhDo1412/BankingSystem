using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingDatabase.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    IsSuccess = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcountInfoId = table.Column<int>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountDetails_AccountInfos_AcountInfoId",
                        column: x => x.AcountInfoId,
                        principalTable: "AccountInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountInfos",
                columns: new[] { "Id", "AccountName", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "AccountA", false },
                    { 2, "AccountB", false }
                });

            migrationBuilder.InsertData(
                table: "TransactionHistories",
                columns: new[] { "Id", "AccountId", "Amount", "Currency", "IsSuccess", "Message" },
                values: new object[,]
                {
                    { 1, 1, 1000m, "USD", true, null },
                    { 2, 1, 1000m, "MYR", true, null },
                    { 3, 2, 1000000m, "VND", true, null },
                    { 4, 2, 1000000m, "BAHT", true, null },
                    { 5, 2, 3000m, "USD", true, null }
                });

            migrationBuilder.InsertData(
                table: "AccountDetails",
                columns: new[] { "Id", "AcountInfoId", "Balance", "Currency", "IsDeleted" },
                values: new object[,]
                {
                    { 1, 1, 1000m, "USD", false },
                    { 2, 1, 1000m, "MYR", false },
                    { 3, 2, 1000000m, "VND", false },
                    { 4, 2, 1000000m, "BAHT", false },
                    { 5, 2, 3000m, "USD", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetails_AcountInfoId",
                table: "AccountDetails",
                column: "AcountInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDetails");

            migrationBuilder.DropTable(
                name: "TransactionHistories");

            migrationBuilder.DropTable(
                name: "AccountInfos");
        }
    }
}
