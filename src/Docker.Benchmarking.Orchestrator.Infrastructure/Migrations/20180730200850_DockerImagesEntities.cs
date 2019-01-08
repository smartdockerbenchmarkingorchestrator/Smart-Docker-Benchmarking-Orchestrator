using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class DockerImagesEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DockerImage",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: false),
                    ImageTag = table.Column<string>(nullable: false),
                    PrivateRepository = table.Column<bool>(nullable: false),
                    PrivateRepositoryHost = table.Column<string>(nullable: true),
                    PrivateRepositoryUsername = table.Column<string>(nullable: true),
                    PrivateRepositoryPassword = table.Column<string>(nullable: true),
                    ExternalPort = table.Column<int>(nullable: true),
                    InternalPort = table.Column<int>(nullable: true),
                    ImageType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerImage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DockerImage");
        }
    }
}
