using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class NewContainerMetrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContainerMetric",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    ContainerId = table.Column<string>(nullable: false),
                    ContainerName = table.Column<string>(nullable: false),
                    JsonDockerResponse = table.Column<string>(nullable: false),
                    DockerDateTimestamp = table.Column<DateTime>(nullable: false),
                    ApplicationTestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerMetric", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerMetric_ApplicationTests_ApplicationTestId",
                        column: x => x.ApplicationTestId,
                        principalTable: "ApplicationTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ContainerMetric_ApplicationTestId",
                table: "ContainerMetric",
                column: "ApplicationTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerMetric");

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 35, 58, 125, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 35, 58, 125, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 35, 58, 125, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 35, 58, 125, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 8, 10, 35, 58, 125, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
