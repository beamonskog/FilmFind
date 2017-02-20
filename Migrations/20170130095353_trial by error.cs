using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FilmFind.Migrations
{
    public partial class trialbyerror : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserID",
                table: "UserMovies");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserMovies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovies_UserID",
                table: "UserMovies",
                newName: "IX_UserMovies_UserId");

            migrationBuilder.AddColumn<int>(
                name: "MyFruitId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Banana",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banana", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MyFruitId",
                table: "AspNetUsers",
                column: "MyFruitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Banana_MyFruitId",
                table: "AspNetUsers",
                column: "MyFruitId",
                principalTable: "Banana",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Banana_MyFruitId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies");

            migrationBuilder.DropTable(
                name: "Banana");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MyFruitId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MyFruitId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserMovies",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovies_UserId",
                table: "UserMovies",
                newName: "IX_UserMovies_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserID",
                table: "UserMovies",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
