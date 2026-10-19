using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClinc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clinc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClincCode = table.Column<int>(type: "int", nullable: false),
                    ClincName = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Specialization = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    DoctorNo = table.Column<int>(type: "int", nullable: false),
                    NumberOfPatients = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinc", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clinc");
        }
    }
}
