using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MentorsDiary.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUserDivision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Divisions",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Name", "UserCreated", "UserUpdated" },
                values: new object[,]
                {
                    { 1, null, null, "Division1", null, null },
                    { 2, null, null, "Division2", null, null },
                    { 3, null, null, "Division3", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "BirthDate", "DateCreated", "DateUpdated", "DivisionId", "Email", "ImagePath", "Name", "Password", "Phone", "Role", "UserCreated", "UserUpdated" },
                values: new object[,]
                {
                    { 1, null, null, null, null, 1, null, null, "user1", "user1", null, 1, null, null },
                    { 2, null, null, null, null, 2, null, null, "user2", "user2", null, 2, null, null },
                    { 3, null, null, null, null, 3, null, null, "user3", "user3", null, 2, null, null },
                    { 4, null, null, null, null, 2, null, null, "user4", "user4", null, 3, null, null },
                    { 5, null, null, null, null, 2, null, null, "user5", "user5", null, 3, null, null },
                    { 6, null, null, null, null, 2, null, null, "user6", "user6", null, 3, null, null },
                    { 7, null, null, null, null, 3, null, null, "user7", "user7", null, 3, null, null },
                    { 8, null, null, null, null, 3, null, null, "user8", "user8", null, 3, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
