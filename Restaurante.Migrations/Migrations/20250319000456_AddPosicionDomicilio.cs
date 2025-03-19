using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class AddPosicionDomicilio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "delivery_id",
                table: "pedidos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<double>(
                name: "lat",
                table: "domicilios",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "lng",
                table: "domicilios",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lat",
                table: "domicilios");

            migrationBuilder.DropColumn(
                name: "lng",
                table: "domicilios");

            migrationBuilder.AlterColumn<long>(
                name: "delivery_id",
                table: "pedidos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
