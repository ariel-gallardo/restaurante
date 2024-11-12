using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class ProductoIngredienteUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(4120),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(4138));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(631),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(1076));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 382, DateTimeKind.Utc).AddTicks(6444),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 152, DateTimeKind.Utc).AddTicks(7228));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "productos",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(569),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(9840));

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "productos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "productos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(8497),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(7982));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(2361),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(2523));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(4394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 154, DateTimeKind.Utc).AddTicks(3241));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "ingredientes",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(6614),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(6486));

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "ingredientes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "ingredientes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 382, DateTimeKind.Utc).AddTicks(8813),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 152, DateTimeKind.Utc).AddTicks(9551));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(2751),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 154, DateTimeKind.Utc).AddTicks(1804));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "ingredientes");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "ingredientes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(4138),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(4120));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(1076),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 152, DateTimeKind.Utc).AddTicks(7228),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 382, DateTimeKind.Utc).AddTicks(6444));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "productos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(9840),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(569));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(7982),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(8497));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(2523),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(2361));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 154, DateTimeKind.Utc).AddTicks(3241),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(4394));

            migrationBuilder.AlterColumn<string>(
                name: "unidad",
                table: "ingredientes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 153, DateTimeKind.Utc).AddTicks(6486),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(6614));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 152, DateTimeKind.Utc).AddTicks(9551),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 382, DateTimeKind.Utc).AddTicks(8813));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 14, 24, 14, 154, DateTimeKind.Utc).AddTicks(1804),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(2751));
        }
    }
}
