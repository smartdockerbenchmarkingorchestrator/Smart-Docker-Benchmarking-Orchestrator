using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class BenchmarkTimeLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerMetric_ApplicationTests_ApplicationTestId",
                table: "ContainerMetric");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContainerMetric",
                table: "ContainerMetric");

            migrationBuilder.RenameTable(
                name: "ContainerMetric",
                newName: "ContainerMetrics");

            migrationBuilder.RenameIndex(
                name: "IX_ContainerMetric_ApplicationTestId",
                table: "ContainerMetrics",
                newName: "IX_ContainerMetrics_ApplicationTestId");

            migrationBuilder.AddColumn<int>(
                name: "BenchmarkTimeLength",
                table: "ApplicationTests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContainerMetrics",
                table: "ContainerMetrics",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 47, 28, 69, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 47, 28, 69, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 47, 28, 69, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 47, 28, 69, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 47, 28, 68, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerMetrics_ApplicationTests_ApplicationTestId",
                table: "ContainerMetrics",
                column: "ApplicationTestId",
                principalTable: "ApplicationTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerMetrics_ApplicationTests_ApplicationTestId",
                table: "ContainerMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContainerMetrics",
                table: "ContainerMetrics");

            migrationBuilder.DropColumn(
                name: "BenchmarkTimeLength",
                table: "ApplicationTests");

            migrationBuilder.RenameTable(
                name: "ContainerMetrics",
                newName: "ContainerMetric");

            migrationBuilder.RenameIndex(
                name: "IX_ContainerMetrics_ApplicationTestId",
                table: "ContainerMetric",
                newName: "IX_ContainerMetric_ApplicationTestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContainerMetric",
                table: "ContainerMetric",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 37, 50, 644, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 37, 50, 644, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 37, 50, 644, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 37, 50, 644, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 37, 50, 643, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerMetric_ApplicationTests_ApplicationTestId",
                table: "ContainerMetric",
                column: "ApplicationTestId",
                principalTable: "ApplicationTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
