using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstOne.Cadastros.Infra.Data.Migrations
{
    public partial class PopulateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pessoa",
                columns: new[] { "Id", "Nome" },
                values: new object[] { new Guid("cf56b7e5-390f-44a4-b44b-9517f7e619ba"), "Tester" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Email", "PessoaId", "Senha" },
                values: new object[] { new Guid("fc127929-ef16-4287-96ce-c8e2c8a051c2"), "tester@tester.com", new Guid("cf56b7e5-390f-44a4-b44b-9517f7e619ba"), "12345" });

            migrationBuilder.InsertData(
                table: "UsuarioClaim",
                columns: new[] { "Id", "Endpoint", "EntidadeEnum", "UsuarioId" },
                values: new object[] { new Guid("6226d62f-a747-4ba2-a137-1a4e9c44a613"), "Add, Update, Delete", 0, new Guid("fc127929-ef16-4287-96ce-c8e2c8a051c2") });

            migrationBuilder.InsertData(
                table: "UsuarioClaim",
                columns: new[] { "Id", "Endpoint", "EntidadeEnum", "UsuarioId" },
                values: new object[] { new Guid("7a1a80af-f3db-4787-b94e-9afd7cd368fe"), "Add, Update, Delete, Claims", 1, new Guid("fc127929-ef16-4287-96ce-c8e2c8a051c2") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsuarioClaim",
                keyColumn: "Id",
                keyValue: new Guid("6226d62f-a747-4ba2-a137-1a4e9c44a613"));

            migrationBuilder.DeleteData(
                table: "UsuarioClaim",
                keyColumn: "Id",
                keyValue: new Guid("7a1a80af-f3db-4787-b94e-9afd7cd368fe"));

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: new Guid("fc127929-ef16-4287-96ce-c8e2c8a051c2"));

            migrationBuilder.DeleteData(
                table: "Pessoa",
                keyColumn: "Id",
                keyValue: new Guid("cf56b7e5-390f-44a4-b44b-9517f7e619ba"));
        }
    }
}
