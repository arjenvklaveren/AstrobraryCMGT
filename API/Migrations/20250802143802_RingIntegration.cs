using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RingIntegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceBodies_RingSystem_RingSystemId",
                table: "SpaceBodies");

            migrationBuilder.DropTable(
                name: "RingSystem");

            migrationBuilder.DropIndex(
                name: "IX_SpaceBodies_RingSystemId",
                table: "SpaceBodies");

            migrationBuilder.RenameColumn(
                name: "RingSystemId",
                table: "SpaceBodies",
                newName: "RingSize");

            migrationBuilder.AddColumn<string>(
                name: "RingDetailColorHex",
                table: "SpaceBodies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RingDistance",
                table: "SpaceBodies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RingMainColorHex",
                table: "SpaceBodies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RingSubColorHex",
                table: "SpaceBodies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RingDetailColorHex",
                table: "SpaceBodies");

            migrationBuilder.DropColumn(
                name: "RingDistance",
                table: "SpaceBodies");

            migrationBuilder.DropColumn(
                name: "RingMainColorHex",
                table: "SpaceBodies");

            migrationBuilder.DropColumn(
                name: "RingSubColorHex",
                table: "SpaceBodies");

            migrationBuilder.RenameColumn(
                name: "RingSize",
                table: "SpaceBodies",
                newName: "RingSystemId");

            migrationBuilder.CreateTable(
                name: "RingSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyId = table.Column<int>(type: "int", nullable: false),
                    Diameter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RingSystem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpaceBodies_RingSystemId",
                table: "SpaceBodies",
                column: "RingSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceBodies_RingSystem_RingSystemId",
                table: "SpaceBodies",
                column: "RingSystemId",
                principalTable: "RingSystem",
                principalColumn: "Id");
        }
    }
}
