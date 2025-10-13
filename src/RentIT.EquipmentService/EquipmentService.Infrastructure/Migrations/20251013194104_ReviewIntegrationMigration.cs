using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReviewIntegrationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "EquipmentItems",
                type: "numeric(3,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ReviewCount",
                table: "EquipmentItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "EquipmentItems");

            migrationBuilder.DropColumn(
                name: "ReviewCount",
                table: "EquipmentItems");
        }
    }
}
