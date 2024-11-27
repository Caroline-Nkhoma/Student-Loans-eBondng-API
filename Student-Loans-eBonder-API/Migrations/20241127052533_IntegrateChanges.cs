using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentLoanseBonderAPI.Migrations
{
    /// <inheritdoc />
    public partial class IntegrateChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string[]>(
                name: "other_names",
                table: "user",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string>(
                name: "surname",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "academic_year",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bank_account_name",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bank_account_number",
                table: "student",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bank_name",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "branch_name",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "date_of_birth",
                table: "student",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "district",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "guardian_id",
                table: "student",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "home_village",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "institution_id",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "national_id_number",
                table: "student",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "student",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "postal_address",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "programme_of_study",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "registration_number",
                table: "student",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sex",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "traditional_authority",
                table: "student",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "year_of_study",
                table: "student",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "bonding_period",
                columns: table => new
                {
                    bonding_period_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bonding_period", x => x.bonding_period_id);
                });

            migrationBuilder.CreateTable(
                name: "form",
                columns: table => new
                {
                    form_id = table.Column<string>(type: "text", nullable: false),
                    student_full_name = table.Column<string>(type: "text", nullable: false),
                    loans_board_official_full_name = table.Column<string>(type: "text", nullable: false),
                    institution_admin_full_name = table.Column<string>(type: "text", nullable: false),
                    student_date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    student_sex = table.Column<string>(type: "text", nullable: false),
                    student_postal_address = table.Column<string>(type: "text", nullable: false),
                    student_home_village = table.Column<string>(type: "text", nullable: false),
                    student_traditional_authority = table.Column<string>(type: "text", nullable: false),
                    student_district = table.Column<string>(type: "text", nullable: false),
                    student_phone_number = table.Column<string>(type: "text", nullable: false),
                    student_national_id_number = table.Column<string>(type: "text", nullable: false),
                    student_bank_name = table.Column<string>(type: "text", nullable: false),
                    student_branch_name = table.Column<string>(type: "text", nullable: false),
                    student_bank_account_name = table.Column<string>(type: "text", nullable: false),
                    student_bank_account_number = table.Column<string>(type: "text", nullable: false),
                    guardian_full_name = table.Column<string>(type: "text", nullable: false),
                    guardian_postal_address = table.Column<string>(type: "text", nullable: false),
                    guardian_physical_address = table.Column<string>(type: "text", nullable: false),
                    guardian_home_village = table.Column<string>(type: "text", nullable: false),
                    guardian_traditional_authority = table.Column<string>(type: "text", nullable: false),
                    guardian_district = table.Column<string>(type: "text", nullable: false),
                    guardian_phone_number = table.Column<string>(type: "text", nullable: false),
                    guardian_occupation = table.Column<string>(type: "text", nullable: false),
                    institution_name = table.Column<string>(type: "text", nullable: false),
                    student_programme_of_study = table.Column<string>(type: "text", nullable: false),
                    student_registration_number = table.Column<string>(type: "text", nullable: false),
                    student_academic_year = table.Column<string>(type: "text", nullable: false),
                    student_year_of_study = table.Column<int>(type: "integer", nullable: false),
                    student_national_id_scan = table.Column<string>(type: "text", nullable: false),
                    student_student_id_scan = table.Column<string>(type: "text", nullable: false),
                    student_signature = table.Column<string>(type: "text", nullable: false),
                    loans_board_official_signature = table.Column<string>(type: "text", nullable: false),
                    institution_admin_signature = table.Column<string>(type: "text", nullable: false),
                    tuition_loan_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    upkeep_loan_amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form", x => x.form_id);
                });

            migrationBuilder.CreateTable(
                name: "guardian",
                columns: table => new
                {
                    guardian_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    other_names = table.Column<string[]>(type: "text[]", nullable: false),
                    postal_address = table.Column<string>(type: "text", nullable: false),
                    physical_address = table.Column<string>(type: "text", nullable: false),
                    home_village = table.Column<string>(type: "text", nullable: false),
                    traditional_authority = table.Column<string>(type: "text", nullable: false),
                    district = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    occupation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guardian", x => x.guardian_id);
                });

            migrationBuilder.CreateTable(
                name: "instituion",
                columns: table => new
                {
                    instituion_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    bonding_period_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instituion", x => x.instituion_id);
                    table.ForeignKey(
                        name: "fk_instituion_bonding_period_bonding_period_id",
                        column: x => x.bonding_period_id,
                        principalTable: "bonding_period",
                        principalColumn: "bonding_period_id");
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    text = table.Column<string>(type: "text", nullable: false),
                    form_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment", x => x.comment_id);
                    table.ForeignKey(
                        name: "fk_comment_form_form_id",
                        column: x => x.form_id,
                        principalTable: "form",
                        principalColumn: "form_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_student_guardian_id",
                table: "student",
                column: "guardian_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_institution_id",
                table: "student",
                column: "institution_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_form_id",
                table: "comment",
                column: "form_id");

            migrationBuilder.CreateIndex(
                name: "ix_instituion_bonding_period_id",
                table: "instituion",
                column: "bonding_period_id");

            migrationBuilder.CreateIndex(
                name: "ix_instituion_name",
                table: "instituion",
                column: "name",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_student_guardian_guardian_id",
                table: "student");

            migrationBuilder.DropForeignKey(
                name: "fk_student_instituion_institution_id",
                table: "student");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "guardian");

            migrationBuilder.DropTable(
                name: "instituion");

            migrationBuilder.DropTable(
                name: "form");

            migrationBuilder.DropTable(
                name: "bonding_period");

            migrationBuilder.DropIndex(
                name: "ix_student_guardian_id",
                table: "student");

            migrationBuilder.DropIndex(
                name: "ix_student_institution_id",
                table: "student");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "other_names",
                table: "user");

            migrationBuilder.DropColumn(
                name: "surname",
                table: "user");

            migrationBuilder.DropColumn(
                name: "academic_year",
                table: "student");

            migrationBuilder.DropColumn(
                name: "bank_account_name",
                table: "student");

            migrationBuilder.DropColumn(
                name: "bank_account_number",
                table: "student");

            migrationBuilder.DropColumn(
                name: "bank_name",
                table: "student");

            migrationBuilder.DropColumn(
                name: "branch_name",
                table: "student");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "student");

            migrationBuilder.DropColumn(
                name: "district",
                table: "student");

            migrationBuilder.DropColumn(
                name: "guardian_id",
                table: "student");

            migrationBuilder.DropColumn(
                name: "home_village",
                table: "student");

            migrationBuilder.DropColumn(
                name: "institution_id",
                table: "student");

            migrationBuilder.DropColumn(
                name: "national_id_number",
                table: "student");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "student");

            migrationBuilder.DropColumn(
                name: "postal_address",
                table: "student");

            migrationBuilder.DropColumn(
                name: "programme_of_study",
                table: "student");

            migrationBuilder.DropColumn(
                name: "registration_number",
                table: "student");

            migrationBuilder.DropColumn(
                name: "sex",
                table: "student");

            migrationBuilder.DropColumn(
                name: "traditional_authority",
                table: "student");

            migrationBuilder.DropColumn(
                name: "year_of_study",
                table: "student");
        }
    }
}
