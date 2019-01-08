using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class DatabaseImagetoApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DatabaseImageId",
                table: "Application",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_DatabaseImageId",
                table: "Application",
                column: "DatabaseImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_DockerImage_DatabaseImageId",
                table: "Application",
                column: "DatabaseImageId",
                principalTable: "DockerImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_DockerImage_DatabaseImageId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_DatabaseImageId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "DatabaseImageId",
                table: "Application");
        }
    }
}
