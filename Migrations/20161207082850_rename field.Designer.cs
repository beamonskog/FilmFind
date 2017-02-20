using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FilmFind.Entities;

namespace FilmFind.Migrations
{
    [DbContext(typeof(FilmFindDbContext))]
    [Migration("20161207082850_rename field")]
    partial class renamefield
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FilmFind.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Director");

                    b.Property<string>("Genre");

                    b.Property<string>("IMDBRating");

                    b.Property<string>("Plot");

                    b.Property<string>("PosterURL");

                    b.Property<string>("RottenTomatoesRating");

                    b.Property<string>("Title");

                    b.Property<int>("UserAverageRating");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("AllMovies");
                });

            modelBuilder.Entity("FilmFind.Entities.MyMovie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Added");

                    b.Property<int>("MovieId");

                    b.Property<string>("MyComment");

                    b.Property<int>("MyRating");

                    b.HasKey("Id");

                    b.ToTable("MyMovies");
                });
        }
    }
}
