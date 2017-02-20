using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmFind.Migrations
{
    public partial class changedpostername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterUrl",
                table: "Movies",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Movies",
                newName: "PosterUrl");
        }
    }
}
