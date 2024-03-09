using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OralData.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCoursed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseEnrolledId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CourseEnrolledId",
                table: "AspNetUsers",
                column: "CourseEnrolledId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CoursesEnrolled_CourseEnrolledId",
                table: "AspNetUsers",
                column: "CourseEnrolledId",
                principalTable: "CoursesEnrolled",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CoursesEnrolled_CourseEnrolledId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CourseEnrolledId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourseEnrolledId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "AspNetUsers");
        }
    }
}
