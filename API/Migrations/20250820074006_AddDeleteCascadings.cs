﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteCascadings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceBodies_Astronomers_DiscovererId",
                table: "SpaceBodies");

            migrationBuilder.AlterColumn<int>(
                name: "DiscovererId",
                table: "SpaceBodies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceBodies_Astronomers_DiscovererId",
                table: "SpaceBodies",
                column: "DiscovererId",
                principalTable: "Astronomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceBodies_Astronomers_DiscovererId",
                table: "SpaceBodies");

            migrationBuilder.AlterColumn<int>(
                name: "DiscovererId",
                table: "SpaceBodies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceBodies_Astronomers_DiscovererId",
                table: "SpaceBodies",
                column: "DiscovererId",
                principalTable: "Astronomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
