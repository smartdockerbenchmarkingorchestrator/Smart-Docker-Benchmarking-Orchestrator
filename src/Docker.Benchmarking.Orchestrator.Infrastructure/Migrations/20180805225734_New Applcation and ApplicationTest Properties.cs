using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class NewApplcationandApplicationTestProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationTests_DockerHosts_HostId",
                table: "ApplicationTests");

            migrationBuilder.RenameColumn(
                name: "HostId",
                table: "ApplicationTests",
                newName: "BenchmarkHostId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationTests_HostId",
                table: "ApplicationTests",
                newName: "IX_ApplicationTests_BenchmarkHostId");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationHostId",
                table: "ApplicationTests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "StopAppContainerAfterExperiment",
                table: "ApplicationTests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DelayToStartApplication",
                table: "Application",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 22, 57, 33, 946, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 22, 57, 33, 946, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 22, 57, 33, 946, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 22, 57, 33, 946, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 22, 57, 33, 945, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTests_ApplicationHostId",
                table: "ApplicationTests",
                column: "ApplicationHostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationTests_DockerHosts_ApplicationHostId",
                table: "ApplicationTests",
                column: "ApplicationHostId",
                principalTable: "DockerHosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationTests_DockerHosts_BenchmarkHostId",
                table: "ApplicationTests",
                column: "BenchmarkHostId",
                principalTable: "DockerHosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationTests_DockerHosts_ApplicationHostId",
                table: "ApplicationTests");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationTests_DockerHosts_BenchmarkHostId",
                table: "ApplicationTests");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationTests_ApplicationHostId",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "ApplicationHostId",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "StopAppContainerAfterExperiment",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "DelayToStartApplication",
                table: "Application");

            migrationBuilder.RenameColumn(
                name: "BenchmarkHostId",
                table: "ApplicationTests",
                newName: "HostId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationTests_BenchmarkHostId",
                table: "ApplicationTests",
                newName: "IX_ApplicationTests_HostId");

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 13, 2, 46, 802, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 13, 2, 46, 802, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 13, 2, 46, 802, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 13, 2, 46, 802, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 13, 2, 46, 802, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationTests_DockerHosts_HostId",
                table: "ApplicationTests",
                column: "HostId",
                principalTable: "DockerHosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
