using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowCycle.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToOverhead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CostingOverheads",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CostingOverheads");
        }
    }
}
