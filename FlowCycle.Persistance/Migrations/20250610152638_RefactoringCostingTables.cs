using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowCycle.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringCostingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostingType",
                table: "Costings");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Costings");

            migrationBuilder.DropColumn(
                name: "OverheadName",
                table: "CostingOverheads");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                table: "CostingMaterials");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "CostingMaterials");

            migrationBuilder.RenameColumn(
                name: "OverheadType",
                table: "CostingOverheads",
                newName: "Uom");

            migrationBuilder.RenameColumn(
                name: "CostValue",
                table: "CostingOverheads",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<int>(
                name: "CostingTypeId",
                table: "Costings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Costings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "CostingOverheads",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OverheadTypeId",
                table: "CostingOverheads",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "QtyPerProduct",
                table: "CostingOverheads",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "CostingOverheads",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "CostingMaterials",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CostingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OverheadTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverheadTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CostingTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Прогнозная" },
                    { 2, "Фактическая" }
                });

            migrationBuilder.InsertData(
                table: "OverheadTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Прямой" },
                    { 2, "Косвенный" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Costings_CostingTypeId",
                table: "Costings",
                column: "CostingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Costings_ProjectId",
                table: "Costings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CostingOverheads_OverheadTypeId",
                table: "CostingOverheads",
                column: "OverheadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CostingMaterials_MaterialId",
                table: "CostingMaterials",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostingMaterials_Materials_MaterialId",
                table: "CostingMaterials",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostingOverheads_OverheadTypes_OverheadTypeId",
                table: "CostingOverheads",
                column: "OverheadTypeId",
                principalTable: "OverheadTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Costings_CostingTypes_CostingTypeId",
                table: "Costings",
                column: "CostingTypeId",
                principalTable: "CostingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Costings_Projects_ProjectId",
                table: "Costings",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostingMaterials_Materials_MaterialId",
                table: "CostingMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_CostingOverheads_OverheadTypes_OverheadTypeId",
                table: "CostingOverheads");

            migrationBuilder.DropForeignKey(
                name: "FK_Costings_CostingTypes_CostingTypeId",
                table: "Costings");

            migrationBuilder.DropForeignKey(
                name: "FK_Costings_Projects_ProjectId",
                table: "Costings");

            migrationBuilder.DropTable(
                name: "CostingTypes");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "OverheadTypes");

            migrationBuilder.DropIndex(
                name: "IX_Costings_CostingTypeId",
                table: "Costings");

            migrationBuilder.DropIndex(
                name: "IX_Costings_ProjectId",
                table: "Costings");

            migrationBuilder.DropIndex(
                name: "IX_CostingOverheads_OverheadTypeId",
                table: "CostingOverheads");

            migrationBuilder.DropIndex(
                name: "IX_CostingMaterials_MaterialId",
                table: "CostingMaterials");

            migrationBuilder.DropColumn(
                name: "CostingTypeId",
                table: "Costings");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Costings");

            migrationBuilder.DropColumn(
                name: "OverheadTypeId",
                table: "CostingOverheads");

            migrationBuilder.DropColumn(
                name: "QtyPerProduct",
                table: "CostingOverheads");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "CostingOverheads");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "CostingMaterials");

            migrationBuilder.RenameColumn(
                name: "Uom",
                table: "CostingOverheads",
                newName: "OverheadType");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "CostingOverheads",
                newName: "CostValue");

            migrationBuilder.AddColumn<string>(
                name: "CostingType",
                table: "Costings",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Costings",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "CostingOverheads",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OverheadName",
                table: "CostingOverheads",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                table: "CostingMaterials",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "CostingMaterials",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
