using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DBL.Migrations
{
    /// <inheritdoc />
    public partial class InitialDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Doctors",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Doctors",
                type: "VARCHAR(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "ahmad.ali@gmail.com", "Male", 1, 10 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "khaled.omar@gmail.com", "Male", 2, 8 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "mohammad.ahmad@gmail.com", "Male", 3, 6 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "omar.hassan@gmail.com", "Male", 4, 12 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "yousef.mahmoud@gmail.com", "Male", 5, 15 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "sara.khalil@gmail.com", "Female", 6, 7 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "lina.ahmad@gmail.com", "Female", 7, 5 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "rami.samir@gmail.com", "Male", 8, 9 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "tareq.nasser@gmail.com", "Male", 9, 11 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Email", "Gender", "SpecialtyId", "YearsOfExperience" },
                values: new object[] { "hassan.saleh@gmail.com", "Male", 10, 13 });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "CARD", true, "Cardiology" },
                    { 2, "DERM", true, "Dermatology" },
                    { 3, "PED", true, "Pediatrics" },
                    { 4, "ORTH", true, "Orthopedics" },
                    { 5, "NEUR", true, "Neurology" },
                    { 6, "GYN", true, "Gynecology" },
                    { 7, "OPHT", true, "Ophthalmology" },
                    { 8, "ENT", true, "ENT" },
                    { 9, "SURG", true, "General Surgery" },
                    { 10, "INT", true, "Internal Medicine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Doctors",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Specialty",
                value: "Cardiology");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Specialty",
                value: "Dermatology");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Specialty",
                value: "Pediatrics");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                column: "Specialty",
                value: "Orthopedics");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                column: "Specialty",
                value: "Neurology");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                column: "Specialty",
                value: "Gynecology");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 7,
                column: "Specialty",
                value: "Ophthalmology");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 8,
                column: "Specialty",
                value: "ENT");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 9,
                column: "Specialty",
                value: "General Surgery");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 10,
                column: "Specialty",
                value: "Internal Medicine");
        }
    }
}
