using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class updateddatabasenameandfieldtypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId",
                table: "MyMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyMovies",
                table: "MyMovies");

            migrationBuilder.RenameTable(
                name: "MyMovies",
                newName: "UserMovies");

            migrationBuilder.RenameIndex(
                name: "IX_MyMovies_UserId",
                table: "UserMovies",
                newName: "IX_UserMovies_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserRating",
                table: "UserMovieRatings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "UserComment",
                table: "UserMovies",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMovies",
                table: "UserMovies",
                column: "Id");

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
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMovies",
                table: "UserMovies");

            migrationBuilder.RenameTable(
                name: "UserMovies",
                newName: "MyMovies");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovies_UserId",
                table: "MyMovies",
                newName: "IX_MyMovies_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserRating",
                table: "UserMovieRatings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserComment",
                table: "MyMovies",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyMovies",
                table: "MyMovies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId",
                table: "MyMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
