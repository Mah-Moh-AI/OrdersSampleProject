using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 8, 18, 23, 12, 696, DateTimeKind.Local).AddTicks(4499));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 8, 18, 23, 12, 696, DateTimeKind.Local).AddTicks(4445));
        }
    }
}
