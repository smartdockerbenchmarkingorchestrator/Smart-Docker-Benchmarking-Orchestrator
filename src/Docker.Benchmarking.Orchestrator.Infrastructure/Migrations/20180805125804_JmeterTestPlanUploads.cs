using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class JmeterTestPlanUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApacheJmeterTestId",
                table: "ApplicationTests",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "ApplicationTests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CompletedAt",
                table: "ApplicationTests",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "Started",
                table: "ApplicationTests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartedAt",
                table: "ApplicationTests",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "ApacheJmeterTestId",
                table: "Application",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApacheJmeterTestFiles",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    ApplicationTestId = table.Column<Guid>(nullable: true),
                    ApplicationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApacheJmeterTestFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApacheJmeterTestFiles_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApacheJmeterTestFiles_ApplicationTests_ApplicationTestId",
                        column: x => x.ApplicationTestId,
                        principalTable: "ApplicationTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 12, 58, 3, 851, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 12, 58, 3, 851, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 12, 58, 3, 851, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 12, 58, 3, 851, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 12, 58, 3, 851, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_ApacheJmeterTestFiles_ApplicationId",
                table: "ApacheJmeterTestFiles",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApacheJmeterTestFiles_ApplicationTestId",
                table: "ApacheJmeterTestFiles",
                column: "ApplicationTestId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApacheJmeterTestFiles");

            migrationBuilder.DropColumn(
                name: "ApacheJmeterTestId",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "Started",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "StartedAt",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "ApacheJmeterTestId",
                table: "Application");

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 9, 15, 15, 147, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 9, 15, 15, 147, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 9, 15, 15, 147, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 9, 15, 15, 147, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 5, 9, 15, 15, 146, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
