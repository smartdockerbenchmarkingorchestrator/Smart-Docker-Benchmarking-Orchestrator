using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class ApplicationTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ApplicationType = table.Column<int>(nullable: false),
                    BenchmarkingImageId = table.Column<Guid>(nullable: true),
                    ApplicationImageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_DockerImage_ApplicationImageId",
                        column: x => x.ApplicationImageId,
                        principalTable: "DockerImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_DockerImage_BenchmarkingImageId",
                        column: x => x.BenchmarkingImageId,
                        principalTable: "DockerImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationTests",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HostId = table.Column<Guid>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    ApplicationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationTests_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationTests_DockerHosts_HostId",
                        column: x => x.HostId,
                        principalTable: "DockerHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BenchmarkTestItems",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Elapsed = table.Column<double>(nullable: false),
                    Label = table.Column<string>(nullable: false),
                    ResponseCode = table.Column<int>(nullable: false),
                    ResponseMessage = table.Column<string>(nullable: false),
                    ThreadName = table.Column<string>(nullable: false),
                    DataType = table.Column<string>(nullable: true),
                    Success = table.Column<bool>(nullable: false),
                    FailureMessage = table.Column<string>(nullable: true),
                    Bytes = table.Column<double>(nullable: false),
                    SentBytes = table.Column<double>(nullable: false),
                    GroupThreads = table.Column<double>(nullable: false),
                    AllThreads = table.Column<double>(nullable: false),
                    Latency = table.Column<double>(nullable: false),
                    IdleTime = table.Column<double>(nullable: false),
                    Connect = table.Column<double>(nullable: false),
                    ApplicationTestId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenchmarkTestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenchmarkTestItems_ApplicationTests_ApplicationTestId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Application_ApplicationImageId",
                table: "Application",
                column: "ApplicationImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_BenchmarkingImageId",
                table: "Application",
                column: "BenchmarkingImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTests_ApplicationId",
                table: "ApplicationTests",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTests_HostId",
                table: "ApplicationTests",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkTestItems_ApplicationTestId",
                table: "BenchmarkTestItems",
                column: "ApplicationTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenchmarkTestItems");

            migrationBuilder.DropTable(
                name: "ApplicationTests");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "CloudProviders",
                keyColumn: "Id",
                keyValue: new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"),
                column: "DateTimeCreated",
                value: new DateTimeOffset(new DateTime(2018, 8, 1, 8, 35, 29, 232, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
