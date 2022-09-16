using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoWeb.Migrations
{
    public partial class CriandoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Matricula = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NIS = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Matricula);
                });

            migrationBuilder.CreateTable(
                name: "Batidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuncionarioMatricula = table.Column<int>(type: "int", nullable: false),
                    TipoBatida = table.Column<int>(type: "int", nullable: false),
                    MatriculaSupervisorAjuste = table.Column<int>(type: "int", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batidas_Funcionarios_FuncionarioMatricula",
                        column: x => x.FuncionarioMatricula,
                        principalTable: "Funcionarios",
                        principalColumn: "Matricula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batidas_FuncionarioMatricula",
                table: "Batidas",
                column: "FuncionarioMatricula");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Batidas");

            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}
