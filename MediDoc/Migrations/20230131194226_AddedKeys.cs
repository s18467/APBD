using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediDoc.Migrations
{
    /// <inheritdoc />
    public partial class AddedKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicament_Medicament_IdMedicament",
                table: "Prescription_Medicament");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicament_Prescription_IdPrescription",
                table: "Prescription_Medicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription_Medicament",
                table: "Prescription_Medicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient",
                table: "Patient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicament",
                table: "Medicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

            migrationBuilder.AddPrimaryKey(
                name: "Prescription_Medicament_pk",
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription" });

            migrationBuilder.AddPrimaryKey(
                name: "Prescription_pk",
                table: "Prescription",
                column: "IdPrescription");

            migrationBuilder.AddPrimaryKey(
                name: "Patient_pk",
                table: "Patient",
                column: "IdPatient");

            migrationBuilder.AddPrimaryKey(
                name: "Medicament_pk",
                table: "Medicament",
                column: "IdMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "Doctor_pk",
                table: "Doctor",
                column: "IdDoctor");

            migrationBuilder.AddForeignKey(
                name: "Prescription_Doctor",
                table: "Prescription",
                column: "IdDoctor",
                principalTable: "Doctor",
                principalColumn: "IdDoctor");

            migrationBuilder.AddForeignKey(
                name: "Prescription_Patient",
                table: "Prescription",
                column: "IdPatient",
                principalTable: "Patient",
                principalColumn: "IdPatient");

            migrationBuilder.AddForeignKey(
                name: "Prescription_Medicament_Medicament",
                table: "Prescription_Medicament",
                column: "IdMedicament",
                principalTable: "Medicament",
                principalColumn: "IdMedicament");

            migrationBuilder.AddForeignKey(
                name: "Prescription_Medicament_Prescription",
                table: "Prescription_Medicament",
                column: "IdPrescription",
                principalTable: "Prescription",
                principalColumn: "IdPrescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Prescription_Doctor",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "Prescription_Patient",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "Prescription_Medicament_Medicament",
                table: "Prescription_Medicament");

            migrationBuilder.DropForeignKey(
                name: "Prescription_Medicament_Prescription",
                table: "Prescription_Medicament");

            migrationBuilder.DropPrimaryKey(
                name: "Prescription_Medicament_pk",
                table: "Prescription_Medicament");

            migrationBuilder.DropPrimaryKey(
                name: "Prescription_pk",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "Patient_pk",
                table: "Patient");

            migrationBuilder.DropPrimaryKey(
                name: "Medicament_pk",
                table: "Medicament");

            migrationBuilder.DropPrimaryKey(
                name: "Doctor_pk",
                table: "Doctor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription_Medicament",
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription",
                column: "IdPrescription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient",
                table: "Patient",
                column: "IdPatient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicament",
                table: "Medicament",
                column: "IdMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "IdDoctor");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription",
                column: "IdDoctor",
                principalTable: "Doctor",
                principalColumn: "IdDoctor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription",
                column: "IdPatient",
                principalTable: "Patient",
                principalColumn: "IdPatient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicament_Medicament_IdMedicament",
                table: "Prescription_Medicament",
                column: "IdMedicament",
                principalTable: "Medicament",
                principalColumn: "IdMedicament",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicament_Prescription_IdPrescription",
                table: "Prescription_Medicament",
                column: "IdPrescription",
                principalTable: "Prescription",
                principalColumn: "IdPrescription",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
