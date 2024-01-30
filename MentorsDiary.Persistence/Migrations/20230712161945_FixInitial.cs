using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorsDiary.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Users",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Students",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Groups",
                newName: "DateUpdated");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "Students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Sex",
                table: "Parents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Groups",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "Groups",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Users",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Students",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Groups",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
