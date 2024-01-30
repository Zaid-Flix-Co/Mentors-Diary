using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorsDiary.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GroupEventStudentsId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GroupEventStudents",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupEventStudents",
                table: "GroupEventStudents",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupEventStudents",
                table: "GroupEventStudents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupEventStudents");
        }
    }
}
