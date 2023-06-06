using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spider.Migrations
{
    /// <inheritdoc />
    public partial class addassignedroutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "AppAssignedRoutes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    yandexid = table.Column<string>(name: "yandex_id", type: "nvarchar(max)", nullable: false),
                    vehicleid = table.Column<string>(name: "vehicle_id", type: "nvarchar(max)", nullable: false),
                    drivername = table.Column<string>(name: "driver_name", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAssignedRoutes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
                name: "AppAssignedRoutes");

         
        }
    }
}
