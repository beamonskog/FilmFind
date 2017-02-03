using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class removedMyMovies : Migration
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
                newName: "MyMovie");

            migrationBuilder.RenameIndex(
                name: "IX_MyMovies_UserId",
                table: "MyMovie",
                newName: "IX_MyMovie_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyMovie",
                table: "MyMovie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MyMovie_AspNetUsers_UserId",
                table: "MyMovie",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyMovie_AspNetUsers_UserId",
                table: "MyMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyMovie",
                table: "MyMovie");

            migrationBuilder.RenameTable(
                name: "MyMovie",
                newName: "MyMovies");

            migrationBuilder.RenameIndex(
                name: "IX_MyMovie_UserId",
                table: "MyMovies",
                newName: "IX_MyMovies_UserId");

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
