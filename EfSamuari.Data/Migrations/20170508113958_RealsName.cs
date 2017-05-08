using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfSamuari.Data.Migrations
{
    public partial class RealsName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samurais_SecretIdentity_SecretIdentityId",
                table: "Samurais");

            migrationBuilder.DropIndex(
                name: "IX_Samurais_SecretIdentityId",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "SecretIdentityId",
                table: "Samurais");

            migrationBuilder.AddColumn<int>(
                name: "SamuraiId",
                table: "SecretIdentity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SecretIdentity_SamuraiId",
                table: "SecretIdentity",
                column: "SamuraiId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SecretIdentity_Samurais_SamuraiId",
                table: "SecretIdentity",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecretIdentity_Samurais_SamuraiId",
                table: "SecretIdentity");

            migrationBuilder.DropIndex(
                name: "IX_SecretIdentity_SamuraiId",
                table: "SecretIdentity");

            migrationBuilder.DropColumn(
                name: "SamuraiId",
                table: "SecretIdentity");

            migrationBuilder.AddColumn<int>(
                name: "SecretIdentityId",
                table: "Samurais",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Samurais_SecretIdentityId",
                table: "Samurais",
                column: "SecretIdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Samurais_SecretIdentity_SecretIdentityId",
                table: "Samurais",
                column: "SecretIdentityId",
                principalTable: "SecretIdentity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
