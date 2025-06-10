using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlowCycle.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddCosting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Costings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProductCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CostingType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Uom = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostingLabors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CostingId = table.Column<int>(type: "integer", nullable: false),
                    LaborName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Hours = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    HourRate = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostingLabors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostingLabors_Costings_CostingId",
                        column: x => x.CostingId,
                        principalTable: "Costings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostingMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CostingId = table.Column<int>(type: "integer", nullable: false),
                    MaterialName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MaterialCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Uom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    QtyPerProduct = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostingMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostingMaterials_Costings_CostingId",
                        column: x => x.CostingId,
                        principalTable: "Costings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostingOverheads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CostingId = table.Column<int>(type: "integer", nullable: false),
                    OverheadName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OverheadType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CostValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostingOverheads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostingOverheads_Costings_CostingId",
                        column: x => x.CostingId,
                        principalTable: "Costings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostingLabors_CostingId",
                table: "CostingLabors",
                column: "CostingId");

            migrationBuilder.CreateIndex(
                name: "IX_CostingMaterials_CostingId",
                table: "CostingMaterials",
                column: "CostingId");

            migrationBuilder.CreateIndex(
                name: "IX_CostingOverheads_CostingId",
                table: "CostingOverheads",
                column: "CostingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostingLabors");

            migrationBuilder.DropTable(
                name: "CostingMaterials");

            migrationBuilder.DropTable(
                name: "CostingOverheads");

            migrationBuilder.DropTable(
                name: "Costings");
        }
    }
}
