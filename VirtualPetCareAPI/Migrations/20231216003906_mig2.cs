using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualPetCareAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Pets",
                newName: "PetName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pets",
                newName: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PetName",
                table: "Pets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PetId",
                table: "Pets",
                newName: "Id");
        }
    }
}
