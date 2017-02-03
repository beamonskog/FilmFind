using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMovies_AspNetUsers_UserId",
                table: "FavoriteMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_HypedMovies_AspNetUsers_UserId",
                table: "HypedMovies");

            migrationBuilder.DropIndex(
                name: "IX_HypedMovies_UserId",
                table: "HypedMovies");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteMovies_UserId",
                table: "FavoriteMovies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HypedMovies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FavoriteMovies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HypedMovies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FavoriteMovies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HypedMovies_UserId",
                table: "HypedMovies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMovies_UserId",
                table: "FavoriteMovies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMovies_AspNetUsers_UserId",
                table: "FavoriteMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HypedMovies_AspNetUsers_UserId",
                table: "HypedMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
