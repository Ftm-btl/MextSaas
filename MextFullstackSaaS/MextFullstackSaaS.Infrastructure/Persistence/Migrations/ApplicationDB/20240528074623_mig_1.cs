using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MextFullstackSaaS.Infrastructure.Persistence.Migrations.ApplicationDB
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66cae961-6bd9-4b82-8f43-807708e9aa62", "AQAAAAIAAYagAAAAEFSwGfdGTi8MuU5aqRkNilQAbX+eEVHG3Vf2P5YlLeKv4Wt8Cy65ZKW16uPtLubX6w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0229a7ab-9d44-4acc-b015-7e1b055c3c44", "AQAAAAIAAYagAAAAEKoV0wYuyFv39yAVFKji+uShVZxJ+cjbQrMtHWNmlIYe/nbcjryMqvpvZXQv7DAadA==" });
        }
    }
}
