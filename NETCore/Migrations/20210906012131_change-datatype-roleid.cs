using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class changedatatyperoleid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accounts_tb_m_roles_RoleId1",
                table: "tb_m_accounts");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_accounts_RoleId1",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "tb_m_accounts");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "tb_m_accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_accounts_RoleId",
                table: "tb_m_accounts",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accounts_tb_m_roles_RoleId",
                table: "tb_m_accounts",
                column: "RoleId",
                principalTable: "tb_m_roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accounts_tb_m_roles_RoleId",
                table: "tb_m_accounts");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_accounts_RoleId",
                table: "tb_m_accounts");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "tb_m_accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                table: "tb_m_accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_accounts_RoleId1",
                table: "tb_m_accounts",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accounts_tb_m_roles_RoleId1",
                table: "tb_m_accounts",
                column: "RoleId1",
                principalTable: "tb_m_roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
