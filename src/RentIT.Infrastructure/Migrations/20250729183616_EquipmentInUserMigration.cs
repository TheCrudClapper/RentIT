using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentIT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentInUserMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "EquipmentItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItems_UserId",
                table: "EquipmentItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentItems_Users_UserId",
                table: "EquipmentItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentItems_Users_UserId",
                table: "EquipmentItems");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentItems_UserId",
                table: "EquipmentItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EquipmentItems");
        }
    }
}
