using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackInformSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePregovorModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregovori_Properties_propertyId",
                table: "Pregovori");

            migrationBuilder.DropIndex(
                name: "IX_Pregovori_propertyId",
                table: "Pregovori");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pregovori_propertyId",
                table: "Pregovori",
                column: "propertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregovori_Properties_propertyId",
                table: "Pregovori",
                column: "propertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
