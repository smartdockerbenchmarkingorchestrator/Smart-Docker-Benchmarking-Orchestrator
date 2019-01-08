using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class Cloud_Provider_Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DockerHosts_CloudProvider_CloudProviderId",
                table: "DockerHosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CloudProvider",
                table: "CloudProvider");

            migrationBuilder.RenameTable(
                name: "CloudProvider",
                newName: "CloudProviders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CloudProviders",
                table: "CloudProviders",
                column: "Id");

            migrationBuilder.InsertData(
                table: "CloudProviders",
                columns: new[] { "Id", "DateTimeCreated", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"), new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "AWS" },
                    { new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"), new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Azure" },
                    { new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"), new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Google Cloud Platform" },
                    { new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"), new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "IBM" },
                    { new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"), new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Private Cloud" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DockerHosts_CloudProviders_CloudProviderId",
                table: "DockerHosts",
                column: "CloudProviderId",
                principalTable: "CloudProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DockerHosts_CloudProviders_CloudProviderId",
                table: "DockerHosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CloudProviders",
                table: "CloudProviders");

            migrationBuilder.DeleteData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"));

            migrationBuilder.DeleteData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"));

            migrationBuilder.DeleteData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"));

            migrationBuilder.DeleteData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"));

            migrationBuilder.DeleteData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"));

            migrationBuilder.RenameTable(
                name: "CloudProviders",
                newName: "CloudProvider");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CloudProvider",
                table: "CloudProvider",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DockerHosts_CloudProvider_CloudProviderId",
                table: "DockerHosts",
                column: "CloudProviderId",
                principalTable: "CloudProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
