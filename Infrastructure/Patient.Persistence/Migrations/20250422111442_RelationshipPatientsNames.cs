using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patient.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipPatientsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Names_Patients_PatientId",
                table: "Names");

            migrationBuilder.DropIndex(
                name: "IX_Names_PatientId",
                table: "Names");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Names");

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NameId",
                table: "Patients",
                column: "NameId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Names_NameId",
                table: "Patients",
                column: "NameId",
                principalTable: "Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Names_NameId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_NameId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Patients");

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId",
                table: "Names",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Names_PatientId",
                table: "Names",
                column: "PatientId",
                unique: true,
                filter: "[PatientId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Names_Patients_PatientId",
                table: "Names",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
