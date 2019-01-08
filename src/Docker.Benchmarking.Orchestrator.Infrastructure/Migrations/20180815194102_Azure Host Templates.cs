using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class AzureHostTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "VMSize",
                table: "AzureHosts");

            migrationBuilder.AddColumn<Guid>(
                name: "TemplateId",
                table: "AzureHosts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AzureVMTemplate",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    VMSize = table.Column<string>(nullable: false),
                    vCPUs = table.Column<double>(nullable: false),
                    Memory = table.Column<double>(nullable: false),
                    DiskSize = table.Column<double>(nullable: false),
                    Template = table.Column<string>(nullable: false),
                    ParametersDefault = table.Column<string>(nullable: false),
                    PricePerHour = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AzureVMTemplate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AzureHosts_TemplateId",
                table: "AzureHosts",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AzureHosts_AzureVMTemplate_TemplateId",
                table: "AzureHosts",
                column: "TemplateId",
                principalTable: "AzureVMTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AzureHosts_AzureVMTemplate_TemplateId",
                table: "AzureHosts");

            migrationBuilder.DropTable(
                name: "AzureVMTemplate");

            migrationBuilder.DropIndex(
                name: "IX_AzureHosts_TemplateId",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "AzureHosts");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AzureHosts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                table: "AzureHosts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AzureHosts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VMSize",
                table: "AzureHosts",
                nullable: false,
                defaultValue: "");
        }
    }
}
