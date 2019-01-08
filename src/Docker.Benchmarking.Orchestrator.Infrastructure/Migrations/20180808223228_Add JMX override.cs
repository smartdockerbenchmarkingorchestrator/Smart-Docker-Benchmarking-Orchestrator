using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class AddJMXoverride : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestPlanVariableOverrides",
                table: "ApplicationTests",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 32, 27, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 32, 27, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 32, 27, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 32, 27, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 32, 27, 772, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestPlanVariableOverrides",
                table: "ApplicationTests");

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 1, 56, 769, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 1, 56, 769, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 1, 56, 769, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 1, 56, 769, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 22, 1, 56, 768, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
