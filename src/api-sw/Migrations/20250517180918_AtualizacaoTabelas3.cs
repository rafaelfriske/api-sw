using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_sw.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoTabelas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TbTarefas_IdStatus",
                table: "TbTarefas",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TbTarefas_IdUsuario",
                table: "TbTarefas",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_TbTarefas_TbLogin_IdUsuario",
                table: "TbTarefas",
                column: "IdUsuario",
                principalTable: "TbLogin",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbTarefas_TbStatus_IdStatus",
                table: "TbTarefas",
                column: "IdStatus",
                principalTable: "TbStatus",
                principalColumn: "IdStatus",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbTarefas_TbLogin_IdUsuario",
                table: "TbTarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_TbTarefas_TbStatus_IdStatus",
                table: "TbTarefas");

            migrationBuilder.DropIndex(
                name: "IX_TbTarefas_IdStatus",
                table: "TbTarefas");

            migrationBuilder.DropIndex(
                name: "IX_TbTarefas_IdUsuario",
                table: "TbTarefas");
        }
    }
}
