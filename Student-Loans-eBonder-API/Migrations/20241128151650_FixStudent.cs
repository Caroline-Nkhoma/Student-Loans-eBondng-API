using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLoanseBonderAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_student_guardian_guardian_id",
                table: "student");

            migrationBuilder.DropForeignKey(
                name: "fk_student_instituion_institution_id",
                table: "student");

            migrationBuilder.AlterColumn<string>(
                name: "institution_id",
                table: "student",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "guardian_id",
                table: "student",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_student_guardian_guardian_id",
                table: "student",
                column: "guardian_id",
                principalTable: "guardian",
                principalColumn: "guardian_id");

            migrationBuilder.AddForeignKey(
                name: "fk_student_instituion_institution_id",
                table: "student",
                column: "institution_id",
                principalTable: "instituion",
                principalColumn: "instituion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_student_guardian_guardian_id",
                table: "student");

            migrationBuilder.DropForeignKey(
                name: "fk_student_instituion_institution_id",
                table: "student");

            migrationBuilder.AlterColumn<string>(
                name: "institution_id",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "guardian_id",
                table: "student",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_student_guardian_guardian_id",
                table: "student",
                column: "guardian_id",
                principalTable: "guardian",
                principalColumn: "guardian_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_student_instituion_institution_id",
                table: "student",
                column: "institution_id",
                principalTable: "instituion",
                principalColumn: "instituion_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
