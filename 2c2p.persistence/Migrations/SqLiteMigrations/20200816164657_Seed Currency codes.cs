using Microsoft.EntityFrameworkCore.Migrations;

namespace _2c2p.persistence.Migrations.SqLiteMigrations
{
    public partial class SeedCurrencycodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CurrencyCodes",
                columns: new[] { "Id", "Code" },
                values: new object[] { 1, "EUR" });

            migrationBuilder.InsertData(
                table: "CurrencyCodes",
                columns: new[] { "Id", "Code" },
                values: new object[] { 2, "USD" });

            migrationBuilder.InsertData(
                table: "CurrencyCodes",
                columns: new[] { "Id", "Code" },
                values: new object[] { 3, "AUD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CurrencyCodes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CurrencyCodes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CurrencyCodes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
