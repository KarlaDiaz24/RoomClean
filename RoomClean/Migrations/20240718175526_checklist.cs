using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomClean.Migrations
{
    public partial class checklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedDescriptions",
                table: "Evidencias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedDescriptions",
                table: "Evidencias");
        }
    }
}
