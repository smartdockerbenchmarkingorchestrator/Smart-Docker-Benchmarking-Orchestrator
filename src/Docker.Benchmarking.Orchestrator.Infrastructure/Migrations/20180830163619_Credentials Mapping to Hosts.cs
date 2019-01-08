using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class CredentialsMappingtoHosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AWSHosts_DockerHosts_DockerHostId",
                table: "AWSHosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AWSHosts_AWSTemplates_TemplateId",
                table: "AWSHosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AzureHosts_DockerHosts_DockerHostId",
                table: "AzureHosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AzureHosts_AzureVMTemplate_TemplateId",
                table: "AzureHosts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateId",
                table: "AzureHosts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DockerHostId",
                table: "AzureHosts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CredentialsId",
                table: "AzureHosts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateId",
                table: "AWSHosts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DockerHostId",
                table: "AWSHosts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CredentialsId",
                table: "AWSHosts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AzureHosts_CredentialsId",
                table: "AzureHosts",
                column: "CredentialsId");

            migrationBuilder.CreateIndex(
                name: "IX_AWSHosts_CredentialsId",
                table: "AWSHosts",
                column: "CredentialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AWSHosts_AWSCredentials_CredentialsId",
                table: "AWSHosts",
                column: "CredentialsId",
                principalTable: "AWSCredentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AWSHosts_DockerHosts_DockerHostId",
                table: "AWSHosts",
                column: "DockerHostId",
                principalTable: "DockerHosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AWSHosts_AWSTemplates_TemplateId",
                table: "AWSHosts",
                column: "TemplateId",
                principalTable: "AWSTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AzureHosts_AzureCredentials_CredentialsId",
                table: "AzureHosts",
                column: "CredentialsId",
                principalTable: "AzureCredentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AzureHosts_DockerHosts_DockerHostId",
                table: "AzureHosts",
                column: "DockerHostId",
                principalTable: "DockerHosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AzureHosts_AzureVMTemplate_TemplateId",
                table: "AzureHosts",
                column: "TemplateId",
                principalTable: "AzureVMTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AWSHosts_AWSCredentials_CredentialsId",
                table: "AWSHosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AWSHosts_DockerHosts_DockerHostId",
                table: "AWSHosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AWSHosts_AWSTemplates_TemplateId",
                table: "AWSHosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AzureHosts_AzureCredentials_CredentialsId",
                table: "AzureHosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AzureHosts_DockerHosts_DockerHostId",
                table: "AzureHosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AzureHosts_AzureVMTemplate_TemplateId",
                table: "AzureHosts");

            migrationBuilder.DropIndex(
                name: "IX_AzureHosts_CredentialsId",
                table: "AzureHosts");

            migrationBuilder.DropIndex(
                name: "IX_AWSHosts_CredentialsId",
                table: "AWSHosts");

            migrationBuilder.DropColumn(
                name: "CredentialsId",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "CredentialsId",
                table: "AWSHosts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateId",
                table: "AzureHosts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "DockerHostId",
                table: "AzureHosts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateId",
                table: "AWSHosts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "DockerHostId",
                table: "AWSHosts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_AWSHosts_DockerHosts_DockerHostId",
                table: "AWSHosts",
                column: "DockerHostId",
                principalTable: "DockerHosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AWSHosts_AWSTemplates_TemplateId",
                table: "AWSHosts",
                column: "TemplateId",
                principalTable: "AWSTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AzureHosts_DockerHosts_DockerHostId",
                table: "AzureHosts",
                column: "DockerHostId",
                principalTable: "DockerHosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AzureHosts_AzureVMTemplate_TemplateId",
                table: "AzureHosts",
                column: "TemplateId",
                principalTable: "AzureVMTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
