using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class RenameAppTestToBenchmark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenchmarkTestItems_ApplicationTests_ApplicationTestId",
                table: "BenchmarkTestItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerMetrics_ApplicationTests_ApplicationTestId",
                table: "ContainerMetrics");

            migrationBuilder.DropTable(
                name: "ApplicationTests");

            migrationBuilder.RenameColumn(
                name: "ApplicationTestId",
                table: "ContainerMetrics",
                newName: "BenchmarkExperimentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContainerMetrics_ApplicationTestId",
                table: "ContainerMetrics",
                newName: "IX_ContainerMetrics_BenchmarkExperimentId");

            migrationBuilder.RenameColumn(
                name: "ApplicationTestId",
                table: "BenchmarkTestItems",
                newName: "BenchmarkExperimentId");

            migrationBuilder.RenameIndex(
                name: "IX_BenchmarkTestItems_ApplicationTestId",
                table: "BenchmarkTestItems",
                newName: "IX_BenchmarkTestItems_BenchmarkExperimentId");

            migrationBuilder.CreateTable(
                name: "BenchmarkExperiments",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    Started = table.Column<bool>(nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    CompletedAt = table.Column<DateTimeOffset>(nullable: false),
                    ApacheJmeterTestId = table.Column<Guid>(nullable: true),
                    TestFileId = table.Column<Guid>(nullable: true),
                    BenchmarkHostId = table.Column<Guid>(nullable: false),
                    ApplicationHostId = table.Column<Guid>(nullable: false),
                    StopAppContainerAfterExperiment = table.Column<bool>(nullable: false),
                    CaptureContainerMetrics = table.Column<bool>(nullable: false),
                    EnvironmentVariables = table.Column<string>(nullable: true),
                    TestPlanVariableOverrides = table.Column<string>(nullable: true),
                    AppContainerId = table.Column<string>(nullable: true),
                    BenchmarkContainerId = table.Column<string>(nullable: true),
                    BenchmarkTimeLength = table.Column<int>(nullable: false),
                    HangfireContainerMetricsJobId = table.Column<string>(nullable: true),
                    ConcurrentUsers = table.Column<int>(nullable: false),
                    SetAsBaseLine = table.Column<bool>(nullable: false),
                    TypeOfTest = table.Column<int>(nullable: false),
                    ResultsCsv = table.Column<string>(type: "Text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenchmarkExperiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenchmarkExperiments_DockerHosts_ApplicationHostId",
                        column: x => x.ApplicationHostId,
                        principalTable: "DockerHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenchmarkExperiments_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenchmarkExperiments_DockerHosts_BenchmarkHostId",
                        column: x => x.BenchmarkHostId,
                        principalTable: "DockerHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenchmarkExperiments_ApacheJmeterTestFiles_TestFileId",
                        column: x => x.TestFileId,
                        principalTable: "ApacheJmeterTestFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkExperiments_ApplicationHostId",
                table: "BenchmarkExperiments",
                column: "ApplicationHostId");

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkExperiments_ApplicationId",
                table: "BenchmarkExperiments",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkExperiments_BenchmarkHostId",
                table: "BenchmarkExperiments",
                column: "BenchmarkHostId");

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkExperiments_TestFileId",
                table: "BenchmarkExperiments",
                column: "TestFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BenchmarkTestItems_BenchmarkExperiments_BenchmarkExperiment~",
                table: "BenchmarkTestItems",
                column: "BenchmarkExperimentId",
                principalTable: "BenchmarkExperiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerMetrics_BenchmarkExperiments_BenchmarkExperimentId",
                table: "ContainerMetrics",
                column: "BenchmarkExperimentId",
                principalTable: "BenchmarkExperiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenchmarkTestItems_BenchmarkExperiments_BenchmarkExperiment~",
                table: "BenchmarkTestItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerMetrics_BenchmarkExperiments_BenchmarkExperimentId",
                table: "ContainerMetrics");

            migrationBuilder.DropTable(
                name: "BenchmarkExperiments");

            migrationBuilder.RenameColumn(
                name: "BenchmarkExperimentId",
                table: "ContainerMetrics",
                newName: "ApplicationTestId");

            migrationBuilder.RenameIndex(
                name: "IX_ContainerMetrics_BenchmarkExperimentId",
                table: "ContainerMetrics",
                newName: "IX_ContainerMetrics_ApplicationTestId");

            migrationBuilder.RenameColumn(
                name: "BenchmarkExperimentId",
                table: "BenchmarkTestItems",
                newName: "ApplicationTestId");

            migrationBuilder.RenameIndex(
                name: "IX_BenchmarkTestItems_BenchmarkExperimentId",
                table: "BenchmarkTestItems",
                newName: "IX_BenchmarkTestItems_ApplicationTestId");

            migrationBuilder.CreateTable(
                name: "ApplicationTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ApacheJmeterTestId = table.Column<Guid>(nullable: true),
                    AppContainerId = table.Column<string>(nullable: true),
                    ApplicationHostId = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    BenchmarkContainerId = table.Column<string>(nullable: true),
                    BenchmarkHostId = table.Column<Guid>(nullable: false),
                    BenchmarkTimeLength = table.Column<int>(nullable: false),
                    CaptureContainerMetrics = table.Column<bool>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false),
                    CompletedAt = table.Column<DateTimeOffset>(nullable: false),
                    ConcurrentUsers = table.Column<int>(nullable: false),
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EnvironmentVariables = table.Column<string>(nullable: true),
                    HangfireContainerMetricsJobId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ResultsCsv = table.Column<string>(type: "Text", nullable: true),
                    SetAsBaseLine = table.Column<bool>(nullable: false),
                    Started = table.Column<bool>(nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(nullable: false),
                    StopAppContainerAfterExperiment = table.Column<bool>(nullable: false),
                    TestFileId = table.Column<Guid>(nullable: true),
                    TestPlanVariableOverrides = table.Column<string>(nullable: true),
                    TypeOfTest = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationTests_DockerHosts_ApplicationHostId",
                        column: x => x.ApplicationHostId,
                        principalTable: "DockerHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationTests_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationTests_DockerHosts_BenchmarkHostId",
                        column: x => x.BenchmarkHostId,
                        principalTable: "DockerHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationTests_ApacheJmeterTestFiles_TestFileId",
                        column: x => x.TestFileId,
                        principalTable: "ApacheJmeterTestFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTests_ApplicationHostId",
                table: "ApplicationTests",
                column: "ApplicationHostId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTests_ApplicationId",
                table: "ApplicationTests",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTests_BenchmarkHostId",
                table: "ApplicationTests",
                column: "BenchmarkHostId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTests_TestFileId",
                table: "ApplicationTests",
                column: "TestFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BenchmarkTestItems_ApplicationTests_ApplicationTestId",
                table: "BenchmarkTestItems",
                column: "ApplicationTestId",
                principalTable: "ApplicationTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerMetrics_ApplicationTests_ApplicationTestId",
                table: "ContainerMetrics",
                column: "ApplicationTestId",
                principalTable: "ApplicationTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
