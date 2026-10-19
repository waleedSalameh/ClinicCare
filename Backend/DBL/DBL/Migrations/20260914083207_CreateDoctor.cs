using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DBL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Specialty = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "FullName", "IsActive", "PhoneNumber", "Specialty" },
                values: new object[,]
                {
                    { 1, "Ahmad Ali", true, "0790000001", "Cardiology" },
                    { 2, "Khaled Omar", true, "0790000002", "Dermatology" },
                    { 3, "Mohammad Ahmad", true, "0790000003", "Pediatrics" },
                    { 4, "Omar Hassan", true, "0790000004", "Orthopedics" },
                    { 5, "Yousef Mahmoud", false, "0790000005", "Neurology" },
                    { 6, "Sara Khalil", true, "0790000006", "Gynecology" },
                    { 7, "Lina Ahmad", true, "0790000007", "Ophthalmology" },
                    { 8, "Rami Samir", false, "0790000008", "ENT" },
                    { 9, "Tareq Nasser", true, "0790000009", "General Surgery" },
                    { 10, "Hassan Saleh", true, "0790000010", "Internal Medicine" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
