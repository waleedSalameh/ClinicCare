using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClinc3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Clinc",
                type: "NVARCHAR(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Clinc");
        }
    }
}
