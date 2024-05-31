using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MextFullstackSaaS.Infrastructure.Persistence.Migrations.ApplicationDB
{
    /// <inheritdoc />
    public partial class OrderDescriptionFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IconDescription",
                table: "Orders",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b0f63120-83d0-4ca6-afb0-289cc3aee817", "AQAAAAIAAYagAAAAEP1XiqyuQPZ4/ldLjQYdp+cJPm6r4yPQY514T7w7yL8V40LcwSjGSH5/s3q4H9+new==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "IconDescription",
                table: "Orders",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66cae961-6bd9-4b82-8f43-807708e9aa62", "AQAAAAIAAYagAAAAEFSwGfdGTi8MuU5aqRkNilQAbX+eEVHG3Vf2P5YlLeKv4Wt8Cy65ZKW16uPtLubX6w==" });
        }
    }
}
