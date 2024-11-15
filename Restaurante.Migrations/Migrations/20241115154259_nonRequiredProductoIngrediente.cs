using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class nonRequiredProductoIngrediente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(3935),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(7026));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(265),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(3408));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 361, DateTimeKind.Utc).AddTicks(6124),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 565, DateTimeKind.Utc).AddTicks(9945));

            migrationBuilder.AlterColumn<string>(
                name: "imagen",
                table: "productos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "productos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 363, DateTimeKind.Utc).AddTicks(1797),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 567, DateTimeKind.Utc).AddTicks(4395));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(9685),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 567, DateTimeKind.Utc).AddTicks(2269));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(2084),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(5191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 363, DateTimeKind.Utc).AddTicks(6508),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 567, DateTimeKind.Utc).AddTicks(8565));

            migrationBuilder.AlterColumn<double>(
                name: "stock_alerta",
                table: "ingredientes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "stock_actual",
                table: "ingredientes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "precio_venta",
                table: "ingredientes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "precio_compra",
                table: "ingredientes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "imagen",
                table: "ingredientes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "ingredientes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(6650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(9699));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 361, DateTimeKind.Utc).AddTicks(8454),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(1968));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 363, DateTimeKind.Utc).AddTicks(4829),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 567, DateTimeKind.Utc).AddTicks(6915));

            migrationBuilder.CreateIndex(
                name: "IX_productos_imagen",
                table: "productos",
                column: "imagen");

            migrationBuilder.CreateIndex(
                name: "IX_ingredientes_imagen",
                table: "ingredientes",
                column: "imagen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_productos_imagen",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_ingredientes_imagen",
                table: "ingredientes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(7026),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(3935));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(3408),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(265));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 565, DateTimeKind.Utc).AddTicks(9945),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 361, DateTimeKind.Utc).AddTicks(6124));

            migrationBuilder.AlterColumn<string>(
                name: "imagen",
                table: "productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "productos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 567, DateTimeKind.Utc).AddTicks(4395),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 363, DateTimeKind.Utc).AddTicks(1797));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 567, DateTimeKind.Utc).AddTicks(2269),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(9685));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(5191),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(2084));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 567, DateTimeKind.Utc).AddTicks(8565),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 363, DateTimeKind.Utc).AddTicks(6508));

            migrationBuilder.AlterColumn<double>(
                name: "stock_alerta",
                table: "ingredientes",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "stock_actual",
                table: "ingredientes",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "precio_venta",
                table: "ingredientes",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "precio_compra",
                table: "ingredientes",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imagen",
                table: "ingredientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "ingredientes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(9699),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 362, DateTimeKind.Utc).AddTicks(6650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 566, DateTimeKind.Utc).AddTicks(1968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 361, DateTimeKind.Utc).AddTicks(8454));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 18, 34, 35, 567, DateTimeKind.Utc).AddTicks(6915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 15, 15, 42, 59, 363, DateTimeKind.Utc).AddTicks(4829));
        }
    }
}
