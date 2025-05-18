using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_sw.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoTabelas4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TarefaRemovida",
                table: "TbTarefas",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TarefaRemovida",
                table: "TbTarefas");
        }
    }
}
