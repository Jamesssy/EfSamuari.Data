using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EfSamuari.Data.Migrations
{
    public partial class Hairstyle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samurais_HairStyles_HairStyleId",
                table: "Samurais");

            migrationBuilder.DropTable(
                name: "HairStyles");

            migrationBuilder.DropIndex(
                name: "IX_Samurais_HairStyleId",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "HairStyleId",
                table: "Samurais");

            migrationBuilder.AddColumn<int>(
                name: "HairStyle",
                table: "Samurais",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HairStyle",
                table: "Samurais");

            migrationBuilder.AddColumn<int>(
                name: "HairStyleId",
                table: "Samurais",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HairStyles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HairStyle = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HairStyles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Samurais_HairStyleId",
                table: "Samurais",
                column: "HairStyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Samurais_HairStyles_HairStyleId",
                table: "Samurais",
                column: "HairStyleId",
                principalTable: "HairStyles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
