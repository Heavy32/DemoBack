using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlowCycle.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStorageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Measure = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    SinglePrice = table.Column<double>(type: "double precision", nullable: false),
                    VAT = table.Column<double>(type: "double precision", nullable: false, comment: "НДС"),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата поступления (Arrival Date)"),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    ArchivedCount = table.Column<int>(type: "integer", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storages_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Storages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Storages_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storages_CategoryId",
                table: "Storages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_ProjectId",
                table: "Storages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_SupplierId",
                table: "Storages",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    Measure = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата поступления"),
                    SinglePrice = table.Column<double>(type: "double precision", nullable: false),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false),
                    VAT = table.Column<double>(type: "double precision", nullable: false, comment: "НДС")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CategoryId",
                table: "Stocks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProjectId",
                table: "Stocks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SupplierId",
                table: "Stocks",
                column: "SupplierId");
        }
    }
}
