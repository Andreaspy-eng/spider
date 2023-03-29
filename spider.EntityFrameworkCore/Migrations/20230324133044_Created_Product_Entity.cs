using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spider.Migrations
{
    /// <inheritdoc />
    public partial class CreatedProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarcodeUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarcodePackage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BushId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightBrutto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CountInPackage = table.Column<long>(type: "bigint", nullable: false),
                    OKEICode = table.Column<long>(type: "bigint", nullable: false),
                    VatRate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProducts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProducts");
        }
    }
}
