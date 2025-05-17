using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_sw.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbLogin",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbLogin", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "TbStatus",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbStatus", x => x.IdStatus);
                });

            migrationBuilder.CreateTable(
                name: "TbTarefas",
                columns: table => new
                {
                    IdTarefa = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataConclusao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IdStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    IdStatusNavigationIdStatus = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbTarefas", x => x.IdTarefa);
                    table.ForeignKey(
                        name: "FK_TbTarefas_TbStatus_IdStatusNavigationIdStatus",
                        column: x => x.IdStatusNavigationIdStatus,
                        principalTable: "TbStatus",
                        principalColumn: "IdStatus");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbTarefas_IdStatusNavigationIdStatus",
                table: "TbTarefas",
                column: "IdStatusNavigationIdStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbLogin");

            migrationBuilder.DropTable(
                name: "TbTarefas");

            migrationBuilder.DropTable(
                name: "TbStatus");
        }
    }
}
