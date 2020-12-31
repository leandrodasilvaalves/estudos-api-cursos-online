using Microsoft.EntityFrameworkCore.Migrations;

namespace Leandro.Estudos.CursosOnline.Api.Migrations
{
    public partial class imagem_aluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Alunos",
                type: "varchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Alunos");
        }
    }
}
