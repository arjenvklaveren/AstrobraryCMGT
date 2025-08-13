using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class DbContextUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceBodies_Astronomer_DiscovererId",
                table: "SpaceBodies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Astronomer",
                table: "Astronomer");

            migrationBuilder.RenameTable(
                name: "Astronomer",
                newName: "Astronomers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Astronomers",
                table: "Astronomers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceBodies_Astronomers_DiscovererId",
                table: "SpaceBodies",
                column: "DiscovererId",
                principalTable: "Astronomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceBodies_Astronomers_DiscovererId",
                table: "SpaceBodies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Astronomers",
                table: "Astronomers");

            migrationBuilder.RenameTable(
                name: "Astronomers",
                newName: "Astronomer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Astronomer",
                table: "Astronomer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceBodies_Astronomer_DiscovererId",
                table: "SpaceBodies",
                column: "DiscovererId",
                principalTable: "Astronomer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
