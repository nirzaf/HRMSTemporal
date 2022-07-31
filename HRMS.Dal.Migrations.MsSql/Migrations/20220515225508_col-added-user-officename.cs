using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Dal.Migrations.MsSql.Migrations
{
    public partial class coladdeduserofficename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficeName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeName",
                table: "Users");
        }
    }
}
