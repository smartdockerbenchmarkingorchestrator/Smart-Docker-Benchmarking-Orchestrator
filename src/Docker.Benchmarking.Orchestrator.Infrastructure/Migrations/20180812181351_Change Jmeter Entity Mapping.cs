using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class ChangeJmeterEntityMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApacheJmeterTestFiles_Application_ApplicationId",
                table: "ApacheJmeterTestFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ApacheJmeterTestFiles_ApplicationTests_ApplicationTestId",
                table: "ApacheJmeterTestFiles");

            migrationBuilder.DropIndex(
                name: "IX_ApacheJmeterTestFiles_ApplicationId",
                table: "ApacheJmeterTestFiles");

            migrationBuilder.DropIndex(
                name: "IX_ApacheJmeterTestFiles_ApplicationTestId",
                table: "ApacheJmeterTestFiles");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "ApacheJmeterTestFiles");

            migrationBuilder.DropColumn(
                name: "ApplicationTestId",
                table: "ApacheJmeterTestFiles");

            migrationBuilder.AddColumn<Guid>(
                name: "TestFileId",
                table: "ApplicationTests",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TestFileId",
                table: "Application",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTests_TestFileId",
                table: "ApplicationTests",
                column: "TestFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_TestFileId",
                table: "Application",
                column: "TestFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_ApacheJmeterTestFiles_TestFileId",
                table: "Application",
                column: "TestFileId",
                principalTable: "ApacheJmeterTestFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationTests_ApacheJmeterTestFiles_TestFileId",
                table: "ApplicationTests",
                column: "TestFileId",
                principalTable: "ApacheJmeterTestFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_ApacheJmeterTestFiles_TestFileId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationTests_ApacheJmeterTestFiles_TestFileId",
                table: "ApplicationTests");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationTests_TestFileId",
                table: "ApplicationTests");

            migrationBuilder.DropIndex(
                name: "IX_Application_TestFileId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "TestFileId",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "TestFileId",
                table: "Application");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                table: "ApacheJmeterTestFiles",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationTestId",
                table: "ApacheJmeterTestFiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApacheJmeterTestFiles_ApplicationId",
                table: "ApacheJmeterTestFiles",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApacheJmeterTestFiles_ApplicationTestId",
                table: "ApacheJmeterTestFiles",
                column: "ApplicationTestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApacheJmeterTestFiles_Application_ApplicationId",
                table: "ApacheJmeterTestFiles",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApacheJmeterTestFiles_ApplicationTests_ApplicationTestId",
                table: "ApacheJmeterTestFiles",
                column: "ApplicationTestId",
                principalTable: "ApplicationTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
