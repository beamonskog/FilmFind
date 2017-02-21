using AutoMapper;
using FilmFind.Entities;
using FilmFind.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static FilmFind.Entities.Models.OmdbMovie;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<ResultTitleMovie, Movie>().
            ForMember(dest => dest.Poster, opt => opt.MapFrom("Poster")).
            ForMember(dest => dest.RottenTomatoesRating, opt => opt.MapFrom("tomatoMeter")).
            ForMember(dest => dest.TomatoeImage, opt => opt.MapFrom(
                source => StringToTomatoeImage(source.tomatoImage))).
            ForMember(dest => dest.Genres, opt => opt.MapFrom(
                source => ExtractGenres(source.Genre)));
    }

    public List<GenreData> ExtractGenres(string input)
    {
        var genreNames = input.Split(new char[] { ',' });
        var genresDoneRight = genreNames.Select(l => new GenreData
        {
            Genre = GenreData.StringToGenre(l)
        }).ToList();
        return genresDoneRight;
    }

    public TomatoeImage StringToTomatoeImage(string input)
    {
        var modifiedInput = input.Trim().ToLower();
        modifiedInput = char.ToUpper(modifiedInput[0]) + modifiedInput.Remove(0, 1);

        TomatoeImage thisTomatoeImage;
        if (!Enum.TryParse(modifiedInput, out thisTomatoeImage))
        {
            thisTomatoeImage = TomatoeImage.Rotten;
            //throw new Exception($"Unable to find {modifiedInput} among existing genres!");
        }

        //TomatoeImage thisTomatoeImage = (TomatoeImage)Enum.Parse(typeof(TomatoeImage), modifiedInput);
        return thisTomatoeImage;
    }
}