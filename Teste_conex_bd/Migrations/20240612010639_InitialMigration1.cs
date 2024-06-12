using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teste_conex_bd.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cd_situacao",
                table: "Diretores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cd_situacao",
                table: "Diretores");
        }
    }
}
