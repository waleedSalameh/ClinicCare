using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedicalService21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Clincid",
                table: "MadicalService",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MadicalService_Clincid",
                table: "MadicalService",
                column: "Clincid");

            migrationBuilder.AddForeignKey(
                name: "FK_MadicalService_Clinc_Clincid",
                table: "MadicalService",
                column: "Clincid",
                principalTable: "Clinc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MadicalService_Clinc_Clincid",
                table: "MadicalService");

            migrationBuilder.DropIndex(
                name: "IX_MadicalService_Clincid",
                table: "MadicalService");

            migrationBuilder.DropColumn(
                name: "Clincid",
                table: "MadicalService");
        }
    }
}
