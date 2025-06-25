using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowCycle.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCostingMaterialTypeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostingMaterials_CostingMaterialTypes_MaterialTypeId",
                table: "CostingMaterials");

            migrationBuilder.RenameColumn(
                name: "MaterialTypeId",
                table: "CostingMaterials",
                newName: "CostingMaterialTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CostingMaterials_MaterialTypeId",
                table: "CostingMaterials",
                newName: "IX_CostingMaterials_CostingMaterialTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostingMaterials_CostingMaterialTypes_CostingMaterialTypeId",
                table: "CostingMaterials",
                column: "CostingMaterialTypeId",
                principalTable: "CostingMaterialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostingMaterials_CostingMaterialTypes_CostingMaterialTypeId",
                table: "CostingMaterials");

            migrationBuilder.RenameColumn(
                name: "CostingMaterialTypeId",
                table: "CostingMaterials",
                newName: "MaterialTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CostingMaterials_CostingMaterialTypeId",
                table: "CostingMaterials",
                newName: "IX_CostingMaterials_MaterialTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostingMaterials_CostingMaterialTypes_MaterialTypeId",
                table: "CostingMaterials",
                column: "MaterialTypeId",
                principalTable: "CostingMaterialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
