using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RingSystemSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RingSystem_SpaceBodies_SpaceBodyId",
                table: "RingSystem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RingSystem",
                table: "RingSystem");

            migrationBuilder.RenameTable(
                name: "RingSystem",
                newName: "RingSystems");

            migrationBuilder.RenameIndex(
                name: "IX_RingSystem_SpaceBodyId",
                table: "RingSystems",
                newName: "IX_RingSystems_SpaceBodyId");

            migrationBuilder.AlterColumn<int>(
                name: "RingSize",
                table: "RingSystems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RingMainColorHex",
                table: "RingSystems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RingDistance",
                table: "RingSystems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RingSystems",
                table: "RingSystems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RingSystems_SpaceBodies_SpaceBodyId",
                table: "RingSystems",
                column: "SpaceBodyId",
                principalTable: "SpaceBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RingSystems_SpaceBodies_SpaceBodyId",
                table: "RingSystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RingSystems",
                table: "RingSystems");

            migrationBuilder.RenameTable(
                name: "RingSystems",
                newName: "RingSystem");

            migrationBuilder.RenameIndex(
                name: "IX_RingSystems_SpaceBodyId",
                table: "RingSystem",
                newName: "IX_RingSystem_SpaceBodyId");

            migrationBuilder.AlterColumn<int>(
                name: "RingSize",
                table: "RingSystem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RingMainColorHex",
                table: "RingSystem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RingDistance",
                table: "RingSystem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RingSystem",
                table: "RingSystem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RingSystem_SpaceBodies_SpaceBodyId",
                table: "RingSystem",
                column: "SpaceBodyId",
                principalTable: "SpaceBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
