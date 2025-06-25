using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowCycle.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCostingMaterialType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostingMaterials_Materials_MaterialId",
                table: "CostingMaterials");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "CostingMaterials",
                newName: "MaterialTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CostingMaterials_MaterialId",
                table: "CostingMaterials",
                newName: "IX_CostingMaterials_MaterialTypeId");

            migrationBuilder.CreateTable(
                name: "CostingMaterialTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostingMaterialTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CostingMaterialTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "сырье" },
                    { 2, "полуфабрикаты" },
                    { 3, "оборудование" },
                    { 4, "комплектующие" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CostingMaterials_CostingMaterialTypes_MaterialTypeId",
                table: "CostingMaterials",
                column: "MaterialTypeId",
                principalTable: "CostingMaterialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostingMaterials_CostingMaterialTypes_MaterialTypeId",
                table: "CostingMaterials");

            migrationBuilder.DropTable(
                name: "CostingMaterialTypes");

            migrationBuilder.RenameColumn(
                name: "MaterialTypeId",
                table: "CostingMaterials",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_CostingMaterials_MaterialTypeId",
                table: "CostingMaterials",
                newName: "IX_CostingMaterials_MaterialId");

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MaterialType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CostingMaterials_Materials_MaterialId",
                table: "CostingMaterials",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
