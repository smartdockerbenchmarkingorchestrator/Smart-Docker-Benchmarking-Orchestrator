using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class AWSHosttoDockerHost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AWSHosts_DockerHostId",
                table: "AWSHosts");

            migrationBuilder.AddColumn<Guid>(
                name: "AwsHostId",
                table: "DockerHosts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AWSHosts_DockerHostId",
                table: "AWSHosts",
                column: "DockerHostId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AWSHosts_DockerHostId",
                table: "AWSHosts");

            migrationBuilder.DropColumn(
                name: "AwsHostId",
                table: "DockerHosts");

            migrationBuilder.CreateIndex(
                name: "IX_AWSHosts_DockerHostId",
                table: "AWSHosts",
                column: "DockerHostId");
        }
    }
}
