using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveNow.Migrations
{
    /// <inheritdoc />
    public partial class RestoreTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Car",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Price = table.Column<float>(type: "real", nullable: false),
                   Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   ProductionYear = table.Column<int>(type: "int", nullable: false),
                   FuelType = table.Column<int>(type: "int", nullable: false),
                   Km = table.Column<int>(type: "int", nullable: false),
                   Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Car", x => x.Id);
               });
            migrationBuilder.CreateTable(
               name: "ReservationPeriods",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                   EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                   CarId = table.Column<int>(type: "int", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_ReservationPeriods", x => x.Id);
                   table.ForeignKey(
                       name: "FK_ReservationPeriods_Car_CarId",
                       column: x => x.CarId,
                       principalTable: "Car",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationPeriods_CarId",
                table: "ReservationPeriods",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Car");
            migrationBuilder.DropTable(
                name: "ReservationPeriods");
        }
    }
}
