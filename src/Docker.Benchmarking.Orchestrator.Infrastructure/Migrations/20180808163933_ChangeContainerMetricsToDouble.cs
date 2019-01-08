using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class ChangeContainerMetricsToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "NetworkOutput",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "NetworkInput",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "MemoryUsage",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "MemoryLimit",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "CPUPercentage",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "BlockOutput",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "BlockInput",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 16, 39, 32, 564, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 16, 39, 32, 564, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 16, 39, 32, 564, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 16, 39, 32, 564, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 16, 39, 32, 563, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NetworkOutput",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "NetworkInput",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "MemoryUsage",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "MemoryLimit",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "CPUPercentage",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "BlockOutput",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "BlockInput",
                table: "ContainerMetrics",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 13, 53, 33, 871, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 13, 53, 33, 871, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 13, 53, 33, 871, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 13, 53, 33, 871, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 13, 53, 33, 871, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
