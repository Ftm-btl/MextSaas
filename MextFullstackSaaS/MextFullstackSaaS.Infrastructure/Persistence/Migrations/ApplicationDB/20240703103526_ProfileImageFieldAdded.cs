using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MextFullstackSaaS.Infrastructure.Persistence.Migrations.ApplicationDB
{
    /// <inheritdoc />
    public partial class ProfileImageFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Users",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfileImage" },
                values: new object[] { "0b58941c-1bd0-490e-b39e-6fb1adc6794a", "AQAAAAIAAYagAAAAEAGx4P4+LtEsbl9MCOM/AGLv/jtRtE7vsjhkZ2cvpxnZ+w3kur1VRdJfnzpEsd2yxA==", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "559fa21d-27f2-4526-9a0a-2cc70a00d204", "AQAAAAIAAYagAAAAEFmRkNucPoBKjU+rW7XHg9diPUR/s4QDxhgw2DcYPuCNjsklEiWdjv100eEqtva3Vg==" });
        }
    }
}
