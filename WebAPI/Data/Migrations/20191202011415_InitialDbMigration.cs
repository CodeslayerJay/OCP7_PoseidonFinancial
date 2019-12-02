using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Data.Migrations
{
    public partial class InitialDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BidList",
                columns: table => new
                {
                    BidListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(maxLength: 20, nullable: true),
                    Type = table.Column<string>(maxLength: 20, nullable: true),
                    BidQuantity = table.Column<double>(nullable: false),
                    AskQuantity = table.Column<double>(nullable: false),
                    Bid = table.Column<double>(nullable: false),
                    Ask = table.Column<double>(nullable: false),
                    Benchmark = table.Column<string>(maxLength: 20, nullable: true),
                    BidListDate = table.Column<DateTime>(nullable: false),
                    Commentary = table.Column<string>(maxLength: 20, nullable: true),
                    Security = table.Column<string>(maxLength: 20, nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: true),
                    Trader = table.Column<string>(maxLength: 20, nullable: true),
                    Book = table.Column<string>(maxLength: 20, nullable: true),
                    CreationName = table.Column<string>(maxLength: 20, nullable: true),
                    RevisionName = table.Column<string>(maxLength: 20, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RevisionDate = table.Column<DateTime>(nullable: false),
                    DealName = table.Column<string>(maxLength: 20, nullable: true),
                    DealType = table.Column<string>(maxLength: 20, nullable: true),
                    SourceListId = table.Column<string>(maxLength: 20, nullable: true),
                    Side = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidList", x => x.BidListId);
                });

            migrationBuilder.CreateTable(
                name: "CurvePoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurveId = table.Column<int>(nullable: false),
                    AsOfDate = table.Column<DateTime>(nullable: false),
                    Term = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurvePoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoodysRating = table.Column<string>(maxLength: 20, nullable: true),
                    SandPRating = table.Column<string>(maxLength: 20, nullable: true),
                    FitchRating = table.Column<string>(maxLength: 20, nullable: true),
                    OrderNumber = table.Column<int>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuleNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(maxLength: 20, nullable: true),
                    JSON = table.Column<string>(maxLength: 20, nullable: true),
                    Template = table.Column<string>(maxLength: 20, nullable: true),
                    SqlString = table.Column<string>(maxLength: 20, nullable: true),
                    SqlPart = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    TradeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(maxLength: 20, nullable: true),
                    Type = table.Column<string>(maxLength: 20, nullable: true),
                    BuyQuantity = table.Column<double>(nullable: false),
                    SellQuantity = table.Column<double>(nullable: false),
                    BuyPrice = table.Column<double>(nullable: false),
                    SellPrice = table.Column<double>(nullable: false),
                    Benchmark = table.Column<string>(maxLength: 20, nullable: true),
                    TradeDate = table.Column<DateTime>(nullable: false),
                    Security = table.Column<string>(maxLength: 20, nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: true),
                    Trader = table.Column<string>(maxLength: 20, nullable: true),
                    Book = table.Column<string>(maxLength: 20, nullable: true),
                    CreationName = table.Column<string>(maxLength: 20, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RevisionName = table.Column<string>(nullable: true),
                    RevisionDate = table.Column<DateTime>(nullable: false),
                    DealName = table.Column<string>(maxLength: 20, nullable: true),
                    DealType = table.Column<string>(maxLength: 20, nullable: true),
                    SourceListId = table.Column<string>(maxLength: 20, nullable: true),
                    Side = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.TradeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 20, nullable: true),
                    Password = table.Column<string>(maxLength: 20, nullable: true),
                    FullName = table.Column<string>(maxLength: 20, nullable: true),
                    Role = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessTokens");

            migrationBuilder.DropTable(
                name: "BidList");

            migrationBuilder.DropTable(
                name: "CurvePoints");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RuleNames");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
