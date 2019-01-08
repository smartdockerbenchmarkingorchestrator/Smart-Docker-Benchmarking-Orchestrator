using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class VariableEntitiesforApplicationandDockerImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationTestPlanVariables",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTestPlanVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationTestPlanVariables_Application_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DockerImageVariables",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerImageVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DockerImageVariables_DockerImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "DockerImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTestPlanVariables_ImageId",
                table: "ApplicationTestPlanVariables",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerImageVariables_ImageId",
                table: "DockerImageVariables",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationTestPlanVariables");

            migrationBuilder.DropTable(
                name: "DockerImageVariables");
        }
    }
}
