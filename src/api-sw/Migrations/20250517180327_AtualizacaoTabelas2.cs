using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_sw.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoTabelas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbTarefas_TbStatus_IdStatusNavigationIdStatus",
                table: "TbTarefas");

            migrationBuilder.DropIndex(
                name: "IX_TbTarefas_IdStatusNavigationIdStatus",
                table: "TbTarefas");

            migrationBuilder.DropColumn(
                name: "IdStatusNavigationIdStatus",
                table: "TbTarefas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdStatusNavigationIdStatus",
                table: "TbTarefas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbTarefas_IdStatusNavigationIdStatus",
                table: "TbTarefas",
                column: "IdStatusNavigationIdStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_TbTarefas_TbStatus_IdStatusNavigationIdStatus",
                table: "TbTarefas",
                column: "IdStatusNavigationIdStatus",
                principalTable: "TbStatus",
                principalColumn: "IdStatus");
        }
    }
}
