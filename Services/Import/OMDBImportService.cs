using AutoMapper;
using FilmFind.Entities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static FilmFind.Entities.Models.OmdbMovie;

namespace FilmFind.Services.Import
{
    public class OMDBImportService : IimportService
    {
        const string BaseUri = "http://www.omdbapi.com/";
        private readonly IMapper _mapper;

        public OMDBImportService(IMapper mapper)
        {
            _mapper = mapper;
        }

        //TODO: onödig? 
        public List<IMovie> GetCompleteMovieList(string searchString, int count)
        {
            var movieSummaries = GetMovieSummariesAsync(searchString, count).Result;
            var movieList = new List<IMovie>();
            foreach (var summary in movieSummaries)
            {
                var thisMovie = GetMovie(summary.Title).Result;
                movieList.Add(thisMovie);
            }

            return movieList;
        }

        public List<MovieSummary> GetMovieSummaries(string searchString, int numberOfResults)
        {
            var x = GetMovieSummariesAsync(searchString, 10);
            return x.Result;
        }

        public Movie GetCompleteMovie(string title)
        {
            var x = GetMovie(title);
            return x.Result;
        }

        //or is it async?
        private async Task<List<MovieSummary>> GetMovieSummariesAsync(string searchString, int numberOfResults)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var response = await client.GetAsync(BaseUri + $"?s={searchString}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var searchResults = JsonConvert.DeserializeObject<SearchResult>(json);
                    return searchResults.Search;
                }
            }

            return null;
        }

        private IEnumerable<IMovie> RemoveDuplicates(IEnumerable<IMovie> searchResults)
        {
            return searchResults.GroupBy(movie => movie.Title).Select(grp => grp.First());
        }

        private async Task<Movie> GetMovie(string title)
        {
            var encodedTitle = WebUtility.UrlEncode(title);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var response = await client.GetAsync(BaseUri + $"?t={encodedTitle}&plot=full&tomatoes=true");
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var searchResult = JsonConvert.DeserializeObject<ResultTitleMovie>(responseJson);
                    var thisMovie = _mapper.Map<ResultTitleMovie, Movie>(searchResult);
                    return thisMovie;
                }
            }
            return new Movie();
        }
    }
}