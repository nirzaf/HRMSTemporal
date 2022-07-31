using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Dal.Migrations.MsSql.Migrations
{
    public partial class seedaddedforuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "LastName", "ModifiedBy", "ModifiedOn", "OfficeName" },
                values: new object[] { 1, "system", new DateTimeOffset(new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vineet", "Yadav", "", new DateTimeOffset(new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "DEL" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
