using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstOne.Cadastros.Infra.Data.Migrations
{
    public partial class AddedRoleToUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Usuario",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: new Guid("fc127929-ef16-4287-96ce-c8e2c8a051c2"),
                column: "Role",
                value: "Motorista");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Usuario");
        }
    }
}
