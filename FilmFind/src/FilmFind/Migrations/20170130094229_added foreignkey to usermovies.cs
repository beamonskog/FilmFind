using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class addedforeignkeytousermovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
