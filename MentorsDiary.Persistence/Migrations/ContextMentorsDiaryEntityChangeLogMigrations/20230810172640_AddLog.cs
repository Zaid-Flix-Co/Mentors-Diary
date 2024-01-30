using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorsDiary.Persistence.Migrations.ContextMentorsDiaryEntityChangeLogMigrations
{
    /// <inheritdoc />
    public partial class AddLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityChangeLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityChangeType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ChangeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityChangeLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityPropertyChangeLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityChangeLogId = table.Column<long>(type: "bigint", nullable: true),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValueDiscription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValueDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPropertyChangeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityPropertyChangeLogs_EntityChangeLogs_EntityChangeLogId",
                        column: x => x.EntityChangeLogId,
                        principalTable: "EntityChangeLogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityChangeLogs_Id",
                table: "EntityChangeLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPropertyChangeLogs_EntityChangeLogId",
                table: "EntityPropertyChangeLogs",
                column: "EntityChangeLogId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPropertyChangeLogs_Id",
                table: "EntityPropertyChangeLogs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityPropertyChangeLogs");

            migrationBuilder.DropTable(
                name: "EntityChangeLogs");
        }
    }
}
