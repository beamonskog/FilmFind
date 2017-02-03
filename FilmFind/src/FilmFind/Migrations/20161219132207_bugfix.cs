using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class bugfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId1",
                table: "MyMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId2",
                table: "MyMovies");

            migrationBuilder.DropIndex(
                name: "IX_MyMovies_UserId1",
                table: "MyMovies");

            migrationBuilder.DropIndex(
                name: "IX_MyMovies_UserId2",
                table: "MyMovies");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "MyMovies");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "MyMovies");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "MyMovies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId2",
                table: "MyMovies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyMovies_UserId1",
                table: "MyMovies",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_MyMovies_UserId2",
                table: "MyMovies",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId1",
                table: "MyMovies",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyMovies_AspNetUsers_UserId2",
                table: "MyMovies",
                column: "UserId2",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
