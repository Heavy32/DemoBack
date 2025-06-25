using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowCycle.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCostingMaterialTypeDefaultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CostingMaterialTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "материал" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CostingMaterialTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
