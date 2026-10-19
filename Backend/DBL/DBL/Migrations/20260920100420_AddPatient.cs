using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    NationalId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "Code", "DateOfBirth", "FullName", "Gender", "NationalId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Amman", 1001, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmad Ali", "Male", "99001122", "0799001122" },
                    { 2, "Zarqa", 1002, new DateTime(1995, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sara Mohammad", "Female", "99112233", "0799112233" },
                    { 3, "Irbid", 1003, new DateTime(1987, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omar Khaled", "Male", "99223344", "0789223344" },
                    { 4, "Amman", 1004, new DateTime(2000, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lina Ahmad", "Female", "99334455", "0779334455" },
                    { 5, "Salt", 1005, new DateTime(1982, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yousef Mahmoud", "Male", "99445566", "0798445566" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
