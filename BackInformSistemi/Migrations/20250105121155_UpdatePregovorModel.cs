using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackInformSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePregovorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregovori_Users_agentId",
                table: "Pregovori");

            migrationBuilder.DropForeignKey(
                name: "FK_Pregovori_Users_buyerId",
                table: "Pregovori");

            migrationBuilder.DropForeignKey(
                name: "FK_Pregovori_Users_menagerId",
                table: "Pregovori");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_agentId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Pregovori_agentId",
                table: "Pregovori");

            migrationBuilder.DropIndex(
                name: "IX_Pregovori_buyerId",
                table: "Pregovori");

            migrationBuilder.DropIndex(
                name: "IX_Pregovori_menagerId",
                table: "Pregovori");

            migrationBuilder.DropColumn(
                name: "agentId",
                table: "Pregovori");

            migrationBuilder.DropColumn(
                name: "buyerId",
                table: "Pregovori");

            migrationBuilder.DropColumn(
                name: "date",
                table: "Pregovori");

            migrationBuilder.DropColumn(
                name: "menagerId",
                table: "Pregovori");

            migrationBuilder.AlterColumn<int>(
                name: "agentId",
                table: "Sales",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_agentId",
                table: "Sales",
                column: "agentId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_agentId",
                table: "Sales");

            migrationBuilder.AlterColumn<int>(
                name: "agentId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "agentId",
                table: "Pregovori",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "buyerId",
                table: "Pregovori",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "date",
                table: "Pregovori",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "menagerId",
                table: "Pregovori",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pregovori_agentId",
                table: "Pregovori",
                column: "agentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregovori_buyerId",
                table: "Pregovori",
                column: "buyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregovori_menagerId",
                table: "Pregovori",
                column: "menagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregovori_Users_agentId",
                table: "Pregovori",
                column: "agentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pregovori_Users_buyerId",
                table: "Pregovori",
                column: "buyerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pregovori_Users_menagerId",
                table: "Pregovori",
                column: "menagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_agentId",
                table: "Sales",
                column: "agentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
