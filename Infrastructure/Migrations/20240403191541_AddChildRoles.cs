using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddChildRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Roles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ParentId",
                table: "Roles",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Roles_ParentId",
                table: "Roles",
                column: "ParentId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Roles_ParentId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ParentId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Roles");
        }
    }
}
