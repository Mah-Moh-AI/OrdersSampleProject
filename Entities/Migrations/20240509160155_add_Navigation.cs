using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_Navigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 9, 19, 1, 55, 330, DateTimeKind.Local).AddTicks(9499));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 9, 19, 1, 55, 330, DateTimeKind.Local).AddTicks(9457));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 9, 13, 49, 29, 777, DateTimeKind.Local).AddTicks(6617));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 9, 13, 49, 29, 777, DateTimeKind.Local).AddTicks(6571));
        }
    }
}
