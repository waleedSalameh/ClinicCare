using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedicalService18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clinc_DoctorNo",
                table: "Clinc",
                column: "DoctorNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinc_Doctors_DoctorNo",
                table: "Clinc",
                column: "DoctorNo",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinc_Doctors_DoctorNo",
                table: "Clinc");

            migrationBuilder.DropIndex(
                name: "IX_Clinc_DoctorNo",
                table: "Clinc");
        }
    }
}
