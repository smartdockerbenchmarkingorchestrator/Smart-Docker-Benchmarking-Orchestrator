using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class VMSizetoAzureandAWSCloudTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VMSizeType",
                table: "AzureVMTemplate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VMSizeType",
                table: "AWSTemplates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VMSizeType",
                table: "AzureVMTemplate");

            migrationBuilder.DropColumn(
                name: "VMSizeType",
                table: "AWSTemplates");
        }
    }
}
