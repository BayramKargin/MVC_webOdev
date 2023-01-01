using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgramlamaOdev2.Migrations
{
    public partial class Role1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Yetki",
                table: "RegisterModel",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Yetki",
                table: "RegisterModel");
        }
    }
}
