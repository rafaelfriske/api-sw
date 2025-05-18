using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_sw.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoTabelas5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "TbTarefas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "TbTarefas");
        }
    }
}
