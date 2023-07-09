using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMap.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    material_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ThermalConductivity = table.Column<double>(type: "REAL", nullable: false),
                    density = table.Column<double>(type: "REAL", nullable: false),
                    humidity = table.Column<double>(type: "REAL", nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    color = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materials", x => x.material_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materials");
        }
    }
}
