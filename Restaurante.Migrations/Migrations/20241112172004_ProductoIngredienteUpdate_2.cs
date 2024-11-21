using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations.Migrations
{
    public partial class ProductoIngredienteUpdate_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(9235),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(4120));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(6361),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(2984),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 382, DateTimeKind.Utc).AddTicks(6444));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(5101),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(569));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(3443),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(8497));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(7757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(2361));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(8544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(4394));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(1319),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(6614));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(4879),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 382, DateTimeKind.Utc).AddTicks(8813));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(7185),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(2751));

            migrationBuilder.CreateIndex(
                name: "IX_productos_nombre",
                table: "productos",
                column: "nombre");

            migrationBuilder.CreateIndex(
                name: "IX_ingredientes_nombre",
                table: "ingredientes",
                column: "nombre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_productos_nombre",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_ingredientes_nombre",
                table: "ingredientes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(4120),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(9235));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "telefonos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(631),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(6361));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 382, DateTimeKind.Utc).AddTicks(6444),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(2984));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(569),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(5101));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "producto_ingrediente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(8497),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(3443));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(2361),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(7757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(4394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(8544));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ingredientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 383, DateTimeKind.Utc).AddTicks(6614),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(1319));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "domicilios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 382, DateTimeKind.Utc).AddTicks(8813),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 993, DateTimeKind.Utc).AddTicks(4879));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "detalle_pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 12, 16, 59, 8, 384, DateTimeKind.Utc).AddTicks(2751),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 12, 17, 20, 3, 994, DateTimeKind.Utc).AddTicks(7185));
        }
    }
}
