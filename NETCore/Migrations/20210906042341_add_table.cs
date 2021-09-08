using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class add_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accounts_tb_m_roles_RoleId",
                table: "tb_m_accounts");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_accounts_RoleId",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "tb_m_accounts");

            migrationBuilder.CreateTable(
                name: "tb_tr_account_roles",
                columns: table => new
                {
                    AccountRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_account_roles", x => x.AccountRoleId);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_roles_tb_m_accounts_AccountNIK",
                        column: x => x.AccountNIK,
                        principalTable: "tb_m_accounts",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_roles_tb_m_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_AccountNIK",
                table: "tb_tr_account_roles",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_RoleId",
                table: "tb_tr_account_roles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_account_roles");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "tb_m_accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
