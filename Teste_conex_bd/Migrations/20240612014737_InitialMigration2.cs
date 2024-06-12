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
                table: "Dvds",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "returnCopy",
                table: "Dvds",
                newName: "ReturnCopy");

            migrationBuilder.RenameColumn(
                name: "rentCopy",
                table: "Dvds",
                newName: "RentCopy");

            migrationBuilder.RenameColumn(
                name: "quantCopias",
                table: "Dvds",
                newName: "QuantCopias");

            migrationBuilder.RenameColumn(
                name: "genero",
                table: "Dvds",
                newName: "Genero");

            migrationBuilder.RenameColumn(
                name: "dtPublicacao",
                table: "Dvds",
                newName: "DtPublicacao");

            migrationBuilder.RenameColumn(
                name: "deletedAt",
                table: "Dvds",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Dvds",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "cd_situacao",
                table: "Dvds",
                newName: "Cd_situacao");

            migrationBuilder.RenameColumn(
                name: "cd_situacao",
                table: "Diretores",
                newName: "Cd_situacao");

            migrationBuilder.AlterColumn<string>(
                name: "ReturnCopy",
                table: "Dvds",
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
                table: "Dvds",
                newName: "titulo");

            migrationBuilder.RenameColumn(
                name: "ReturnCopy",
                table: "Dvds",
                newName: "returnCopy");

            migrationBuilder.RenameColumn(
                name: "RentCopy",
                table: "Dvds",
                newName: "rentCopy");

            migrationBuilder.RenameColumn(
                name: "QuantCopias",
                table: "Dvds",
                newName: "quantCopias");

            migrationBuilder.RenameColumn(
                name: "Genero",
                table: "Dvds",
                newName: "genero");

            migrationBuilder.RenameColumn(
                name: "DtPublicacao",
                table: "Dvds",
                newName: "dtPublicacao");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Dvds",
                newName: "deletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Dvds",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Cd_situacao",
                table: "Dvds",
                newName: "cd_situacao");

            migrationBuilder.RenameColumn(
                name: "Cd_situacao",
                table: "Diretores",
                newName: "cd_situacao");

            migrationBuilder.AlterColumn<string>(
                name: "returnCopy",
                table: "Dvds",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
