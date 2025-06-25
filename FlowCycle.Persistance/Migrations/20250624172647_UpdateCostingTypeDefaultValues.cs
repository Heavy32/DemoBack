using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowCycle.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCostingTypeDefaultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CostingTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Прогнозный");

            migrationBuilder.UpdateData(
                table: "CostingTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Фактический");

            migrationBuilder.InsertData(
                table: "CostingTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Плановый" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CostingTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "CostingTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Прогнозная");

            migrationBuilder.UpdateData(
                table: "CostingTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Фактическая");
        }
    }
}
