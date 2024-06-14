using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teste_conex_bd.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "titulo",
                table: "dvds",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "returnCopy",
                table: "dvds",
                newName: "ReturnCopy");

            migrationBuilder.RenameColumn(
                name: "rentCopy",
                table: "dvds",
                newName: "RentCopy");

            migrationBuilder.RenameColumn(
                name: "quantCopias",
                table: "dvds",
                newName: "QuantCopias");

            migrationBuilder.RenameColumn(
                name: "genero",
                table: "dvds",
                newName: "Genero");

            migrationBuilder.RenameColumn(
                name: "dtPublicacao",
                table: "dvds",
                newName: "DtPublicacao");

            migrationBuilder.RenameColumn(
                name: "deletedAt",
                table: "dvds",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "dvds",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "cd_situacao",
                table: "dvds",
                newName: "Cd_situacao");

            migrationBuilder.RenameColumn(
                name: "cd_situacao",
                table: "diretores",
                newName: "Cd_situacao");

            migrationBuilder.AlterColumn<string>(
                name: "ReturnCopy",
                table: "dvds",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "dvds",
                newName: "titulo");

            migrationBuilder.RenameColumn(
                name: "ReturnCopy",
                table: "dvds",
                newName: "returnCopy");

            migrationBuilder.RenameColumn(
                name: "RentCopy",
                table: "dvds",
                newName: "rentCopy");

            migrationBuilder.RenameColumn(
                name: "QuantCopias",
                table: "dvds",
                newName: "quantCopias");

            migrationBuilder.RenameColumn(
                name: "Genero",
                table: "dvds",
                newName: "genero");

            migrationBuilder.RenameColumn(
                name: "DtPublicacao",
                table: "dvds",
                newName: "dtPublicacao");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "dvds",
                newName: "deletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "dvds",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Cd_situacao",
                table: "dvds",
                newName: "cd_situacao");

            migrationBuilder.RenameColumn(
                name: "Cd_situacao",
                table: "diretores",
                newName: "cd_situacao");

            migrationBuilder.AlterColumn<string>(
                name: "returnCopy",
                table: "dvds",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
