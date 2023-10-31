using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApi_H3_Database.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterPropToStudentCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Character",
                table: "StudentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Character",
                table: "StudentCourses");
        }
    }
}
