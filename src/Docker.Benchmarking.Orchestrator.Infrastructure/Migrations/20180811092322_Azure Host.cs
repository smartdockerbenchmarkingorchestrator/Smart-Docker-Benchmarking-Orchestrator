using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class AzureHost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DockerHosts_CloudProviders_CloudProviderId",
                table: "DockerHosts");

            migrationBuilder.DropTable(
                name: "CloudProviders");

            migrationBuilder.DropIndex(
                name: "IX_DockerHosts_CloudProviderId",
                table: "DockerHosts");

            migrationBuilder.DropColumn(
                name: "CloudProviderId",
                table: "DockerHosts");

            migrationBuilder.AddColumn<Guid>(
                name: "AzureHostId",
                table: "DockerHosts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Memory",
                table: "DockerHosts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Storage",
                table: "DockerHosts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "vCPU",
                table: "DockerHosts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ResultsCsv",
                table: "ApplicationTests",
                type: "Text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AzureHosts",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    ResourceName = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    AzureRegion = table.Column<string>(nullable: false),
                    VMSize = table.Column<string>(nullable: false),
                    DockerHostId = table.Column<Guid>(nullable: true),
                    PricePerHour = table.Column<decimal>(nullable: false),
                    ResourceCreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ResourceDestroyedAt = table.Column<DateTimeOffset>(nullable: false),
                    TenantId = table.Column<string>(nullable: false),
                    ClientId = table.Column<string>(nullable: false),
                    Secret = table.Column<string>(nullable: false),
                    Subscription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AzureHosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AzureHosts_DockerHosts_DockerHostId",
                        column: x => x.DockerHostId,
                        principalTable: "DockerHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AzureHosts_DockerHostId",
                table: "AzureHosts",
                column: "DockerHostId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "AzureHostId",
                table: "DockerHosts");

            migrationBuilder.DropColumn(
                name: "Memory",
                table: "DockerHosts");

            migrationBuilder.DropColumn(
                name: "Storage",
                table: "DockerHosts");

            migrationBuilder.DropColumn(
                name: "vCPU",
                table: "DockerHosts");

            migrationBuilder.DropColumn(
                name: "ResultsCsv",
                table: "ApplicationTests");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudProviderId",
                table: "DockerHosts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CloudProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudProviders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CloudProviders",
                columns: new[] { "Id", "DateTimeCreated", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"), new DateTimeOffset(new DateTime(2018, 8, 9, 17, 8, 16, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "AWS" },
                    { new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"), new DateTimeOffset(new DateTime(2018, 8, 9, 17, 8, 16, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Azure" },
                    { new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"), new DateTimeOffset(new DateTime(2018, 8, 9, 17, 8, 16, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Google Cloud Platform" },
                    { new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"), new DateTimeOffset(new DateTime(2018, 8, 9, 17, 8, 16, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "IBM" },
                    { new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"), new DateTimeOffset(new DateTime(2018, 8, 9, 17, 8, 16, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Private Cloud" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DockerHosts_CloudProviderId",
                table: "DockerHosts",
                column: "CloudProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DockerHosts_CloudProviders_CloudProviderId",
                table: "DockerHosts",
                column: "CloudProviderId",
                principalTable: "CloudProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
