using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class removestringemptydefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("15256d5c-9038-4e98-89f2-664010a847d8"),
                column: "CreateDate",
                value: new DateTime(2024, 5, 16, 13, 13, 44, 989, DateTimeKind.Local).AddTicks(8026));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b0d83c5a-1b71-4e1c-97ec-c59de6bc5c67"),
                column: "CreateDate",
                value: new DateTime(2024, 5, 16, 13, 13, 44, 989, DateTimeKind.Local).AddTicks(8019));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("15256d5c-9038-4e98-89f2-664010a847d8"),
                column: "CreateDate",
                value: new DateTime(2024, 5, 16, 10, 43, 56, 374, DateTimeKind.Local).AddTicks(2417));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b0d83c5a-1b71-4e1c-97ec-c59de6bc5c67"),
                column: "CreateDate",
                value: new DateTime(2024, 5, 16, 10, 43, 56, 374, DateTimeKind.Local).AddTicks(2409));
        }
    }
}
