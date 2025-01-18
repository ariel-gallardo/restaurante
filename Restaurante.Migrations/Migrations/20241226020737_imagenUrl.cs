using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class imagenUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(6377),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(5951));

            migrationBuilder.AddColumn<string>(
                name: "imagen_url",
                table: "usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(2778),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(2288));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 485, DateTimeKind.Utc).AddTicks(8626),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 30, DateTimeKind.Utc).AddTicks(7792));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 487, DateTimeKind.Utc).AddTicks(5163),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(4557));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 487, DateTimeKind.Utc).AddTicks(2846),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(2245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(4496),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(4126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 488, DateTimeKind.Utc).AddTicks(307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(9591));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(9513),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(9039));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(937),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(363));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 487, DateTimeKind.Utc).AddTicks(8441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(7792));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagen_url",
                table: "usuarios");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(5951),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(6377));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(2288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(2778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 30, DateTimeKind.Utc).AddTicks(7792),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 485, DateTimeKind.Utc).AddTicks(8626));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(4557),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 487, DateTimeKind.Utc).AddTicks(5163));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(2245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 487, DateTimeKind.Utc).AddTicks(2846));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(4126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(4496));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(9591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 488, DateTimeKind.Utc).AddTicks(307));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(9039),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(9513));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 31, DateTimeKind.Utc).AddTicks(363),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 486, DateTimeKind.Utc).AddTicks(937));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 21, 19, 0, 22, 32, DateTimeKind.Utc).AddTicks(7792),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 26, 2, 7, 36, 487, DateTimeKind.Utc).AddTicks(8441));
        }
    }
}
