using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class editrelasi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_roles_tb_m_accounts_AccountNIK",
                table: "tb_tr_account_roles");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_account_roles_AccountNIK",
                table: "tb_tr_account_roles");

            migrationBuilder.DropColumn(
                name: "AccountNIK",
                table: "tb_tr_account_roles");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_tr_account_roles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_NIK",
                table: "tb_tr_account_roles",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_roles_tb_m_accounts_NIK",
                table: "tb_tr_account_roles",
                column: "NIK",
                principalTable: "tb_m_accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_roles_tb_m_accounts_NIK",
                table: "tb_tr_account_roles");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_account_roles_NIK",
                table: "tb_tr_account_roles");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_tr_account_roles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNIK",
                table: "tb_tr_account_roles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_AccountNIK",
                table: "tb_tr_account_roles",
                column: "AccountNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_roles_tb_m_accounts_AccountNIK",
                table: "tb_tr_account_roles",
                column: "AccountNIK",
                principalTable: "tb_m_accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
