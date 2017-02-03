using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class updatefieldnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyRating",
                table: "MyMovies",
                newName: "UserRating");

            migrationBuilder.RenameColumn(
                name: "MyComment",
                table: "MyMovies",
                newName: "UserComment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRating",
                table: "MyMovies",
                newName: "MyRating");

            migrationBuilder.RenameColumn(
                name: "UserComment",
                table: "MyMovies",
                newName: "MyComment");
        }
    }
}
