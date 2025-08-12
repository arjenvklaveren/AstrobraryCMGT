using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RingIntegration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "RingSize",
                table: "SpaceBodies");

            migrationBuilder.DropColumn(
                name: "RingSubColorHex",
                table: "SpaceBodies");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "SpaceBodies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RotationSpeed",
                table: "SpaceBodies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RingSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RingDistance = table.Column<int>(type: "int", nullable: true),
                    RingSize = table.Column<int>(type: "int", nullable: true),
                    RingMainColorHex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RingSubColorHex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RingDetailColorHex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpaceBodyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RingSystem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RingSystem_SpaceBodies_SpaceBodyId",
                        column: x => x.SpaceBodyId,
                        principalTable: "SpaceBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RingSystem_SpaceBodyId",
                table: "RingSystem",
                column: "SpaceBodyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RingSystem");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "SpaceBodies");

            migrationBuilder.DropColumn(
                name: "RotationSpeed",
                table: "SpaceBodies");

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

            migrationBuilder.AddColumn<int>(
                name: "RingSize",
                table: "SpaceBodies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RingSubColorHex",
                table: "SpaceBodies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
