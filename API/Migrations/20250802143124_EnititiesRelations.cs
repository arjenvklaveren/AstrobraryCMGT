using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class EnititiesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Astronomer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Astronomer", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "SpaceBodies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscoveryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MainInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mass = table.Column<int>(type: "int", nullable: false),
                    Luminosity = table.Column<int>(type: "int", nullable: false),
                    Diameter = table.Column<int>(type: "int", nullable: false),
                    Velocity = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    DistanceFromParent = table.Column<int>(type: "int", nullable: false),
                    RotationAngle = table.Column<int>(type: "int", nullable: false),
                    AtmosphereThickness = table.Column<int>(type: "int", nullable: false),
                    MainColorHex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubColorHex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscovererId = table.Column<int>(type: "int", nullable: false),
                    RingSystemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceBodies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpaceBodies_Astronomer_DiscovererId",
                        column: x => x.DiscovererId,
                        principalTable: "Astronomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpaceBodies_RingSystem_RingSystemId",
                        column: x => x.RingSystemId,
                        principalTable: "RingSystem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpaceBodies_DiscovererId",
                table: "SpaceBodies",
                column: "DiscovererId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceBodies_RingSystemId",
                table: "SpaceBodies",
                column: "RingSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpaceBodies");

            migrationBuilder.DropTable(
                name: "Astronomer");

            migrationBuilder.DropTable(
                name: "RingSystem");
        }
    }
}
