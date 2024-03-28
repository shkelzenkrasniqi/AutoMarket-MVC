using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarket.Migrations
{
    public partial class motorcycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MotorcycleTypeId",
                table: "Motorcycles",
                newName: "TransmissionType");

            migrationBuilder.RenameColumn(
                name: "MotorcycleTransmissionId",
                table: "Motorcycles",
                newName: "MotorcycleType");

            migrationBuilder.RenameColumn(
                name: "MotorcycleMileageId",
                table: "Motorcycles",
                newName: "Mileage");

            migrationBuilder.RenameColumn(
                name: "MotorcycleFuelTypeId",
                table: "Motorcycles",
                newName: "FuelType");

            migrationBuilder.RenameColumn(
                name: "MotorcycleConditionId",
                table: "Motorcycles",
                newName: "Condition");

            migrationBuilder.RenameColumn(
                name: "MotorcycleColorId",
                table: "Motorcycles",
                newName: "Color");

            migrationBuilder.AddColumn<int>(
                name: "MotorcycleBrandId",
                table: "MotorcycleModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MotorcycleModels_MotorcycleBrandId",
                table: "MotorcycleModels",
                column: "MotorcycleBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_MotorcycleModels_MotorcycleBrands_MotorcycleBrandId",
                table: "MotorcycleModels",
                column: "MotorcycleBrandId",
                principalTable: "MotorcycleBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MotorcycleModels_MotorcycleBrands_MotorcycleBrandId",
                table: "MotorcycleModels");

            migrationBuilder.DropIndex(
                name: "IX_MotorcycleModels_MotorcycleBrandId",
                table: "MotorcycleModels");

            migrationBuilder.DropColumn(
                name: "MotorcycleBrandId",
                table: "MotorcycleModels");

            migrationBuilder.RenameColumn(
                name: "TransmissionType",
                table: "Motorcycles",
                newName: "MotorcycleTypeId");

            migrationBuilder.RenameColumn(
                name: "MotorcycleType",
                table: "Motorcycles",
                newName: "MotorcycleTransmissionId");

            migrationBuilder.RenameColumn(
                name: "Mileage",
                table: "Motorcycles",
                newName: "MotorcycleMileageId");

            migrationBuilder.RenameColumn(
                name: "FuelType",
                table: "Motorcycles",
                newName: "MotorcycleFuelTypeId");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "Motorcycles",
                newName: "MotorcycleConditionId");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Motorcycles",
                newName: "MotorcycleColorId");
        }
    }
}
