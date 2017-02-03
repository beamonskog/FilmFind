using FilmFind.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FilmFind.Services
{
    public class SearchService
    {
        public async Task<List<Movie>> GetMoviesOnline(string searchString)
        {
            //const int searchResults = 5;
            var results = new List<Movie>();
            using (var client = new HttpClient())
            {
                var baseUri = "http://www.omdbapi.com/";
                client.BaseAddress = new Uri(baseUri);
                var response = await client.GetAsync(baseUri + $"?s={searchString}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var searchResults = JsonConvert.DeserializeObject<SearchResult>(json);
                    foreach (var movieShort in searchResults.Search)
                    {
                        var thisMovie = await GetMovieOnline(movieShort.Title);
                        results.Add(thisMovie);
                    }
                }
            }

            return results;
        }

        public async Task<Movie> GetMovieOnline(string searchString)
        {
            using (var client = new HttpClient())
            {
                var baseUri = "http://www.omdbapi.com/";
                client.BaseAddress = new Uri(baseUri);
                var response = await client.GetAsync(baseUri + $"?t={searchString}&tomatoes=true");
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    dynamic deserialized = JsonConvert.DeserializeObject(responseJson);
                    if (string.IsNullOrEmpty((string)deserialized.Error))
                    {
                        return GetMovieFromJson(responseJson);
                    }
                }
            }
            return new Movie();
        }

        private Movie GetMovieFromJson(string jsonString)
        {
            dynamic deserialized = JsonConvert.DeserializeObject(jsonString);
            return new Movie()
            {
                Title = deserialized.Title,
                Genre = deserialized.Genre,
                Director = deserialized.Director,
                Year = deserialized.Year,
                IMDBRating = deserialized.imdbRating,
                RottenTomatoesRating = deserialized.tomatoMeter,
                PosterUrl = deserialized.Poster,
                Plot = deserialized.Plot,
                Created = DateTime.UtcNow
            };


            //MINE
            //public string Title { get; set; }
            //public Genre Genre { get; set; }
            //public string Director { get; set; }
            //public string Description { get; set; }
            //public int MyRating { get; set; }
            //public int Year { get; set; }

            //public int IMDBRating { get; set; }
            //public int RottenTomatoesRating { get; set; }
            //public byte[] Poster { get; set; }

            //THEIRS

            /* http://www.omdbapi.com/?t=alien&y=&plot=short&tomatoes=true&r=json = 
            Actors:"Tom Skerritt, Sigourney Weaver, Veronica Cartwright, Harry Dean Stanton"
            Awards:"Won 1 Oscar. Another 16 wins & 19 nominations."
            BoxOffice:"N/A"
            Country:"UK, USA"
            DVD:"06 Jan 2004"
            Director:"Ridley Scott"
            Genre:"Horror, Sci-Fi"
            Language:"English, Spanish"
            Metascore:"83"
            Plot:"After a space merchant vessel perceives an unknown transmission as distress call, their landing on the source moon finds one of the crew attacked by a mysterious lifeform. Continuing their journey back to Earth with the attacked crew having recovered and the critter deceased, they soon realize that its life cycle has merely begun."
            Poster:"https://images-na.ssl-images-amazon.com/images/M/MV5BNDNhN2IxZWItNGEwYS00ZDNhLThiM2UtODU3NWJlZjBkYjQxXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg"
            Production:"20th Century Fox"
            Rated:"R"
            Released:"22 Jun 1979"
            Response:"True"
            Runtime:"117 min"
            Title:"Alien"
            Type:"movie"
            Website:"http://www.foxmovies.com/index1.html"
            Writer:"Dan O'Bannon (story), Ronald Shusett (story), Dan O'Bannon (screenplay)"
            Year:"1979"
            imdbID:"tt0078748"
            imdbRating:"8.5"
            imdbVotes:"575,751"
            tomatoConsensus:"A modern classic, Alien blends science fiction, horror and bleak poetry into a seamless whole."
            tomatoFresh:"99"
            tomatoImage:"certified"
            tomatoMeter:"97"
            tomatoRating:"9.0"
            tomatoReviews:"102"
            tomatoRotten:"3"
            tomatoURL:"http://www.rottentomatoes.com/m/alien/"
            tomatoUserMeter:"94"
            tomatoUserRating:"3.9"
            tomatoUserReviews:"455453"
             */
        }

        public class SearchResult
        {
            public ResultMovie[] Search { get; set; }
            public string totalResults { get; set; }
            public string Response { get; set; }
        }

        public class ResultMovie
        {
            //"Title\":\"Alien\",\
            //"Year\":\"1979\",\
            //"imdbID\":\"tt0078748\",
            //\"Type\":\"movie\",\
            //"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BNDNhN2IxZWItNGEwYS00ZDNhLThiM2UtODU3NWJlZjBkYjQxXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg\"},{\"Title\":\"Alien 3\",\"Year\":\"1992\",\"imdbID\":\"tt0103644\",\"Type\":\"movie\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BNThhODA5Y2ItMzEzYy00MjE3LTgxYTUtNDIzYjc2ZTllODE0XkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg\"},{\"Title\":\"Alien: Resurrection\",\"Year\":\"1997\",\"imdbID\":\"tt0118583\",\"Type\":\"movie\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BM2YxYmFjYWMtMzBmMC00MTVmLThhMjUtYWU5Yzg2OGQwZjE3XkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg\"},{\"Title\":\"AVP: Alien vs. Predator\",\"Year\":\"2004\",\"imdbID\":\"tt0370263\",\"Type\":\"movie\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BMTU4MjIwMTcyMl5BMl5BanBnXkFtZTYwMTYwNDA3._V1_SX300.jpg\"},{\"Title\":\"My Stepmother Is an Alien\",\"Year\":\"1988\",\"imdbID\":\"tt0095687\",\"Type\":\"movie\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BMTM5OTI0MzM5Nl5BMl5BanBnXkFtZTcwOTcwMjcyMQ@@._V1_SX300.jpg\"},{\"Title\":\"Alien Nation\",\"Year\":\"1988\",\"imdbID\":\"tt0094631\",\"Type\":\"movie\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BMTU5NjYxNjM5MV5BMl5BanBnXkFtZTcwNDk4MTU1MQ@@._V1_SX300.jpg\"
            public string Title { get; set; }
            public string Year { get; set; }
            public string imdbID { get; set; }
            public string Type { get; set; }
            public string Poster { get; set; }
        }

        public class ResultTitleMovie
        {
            string Title { get; set; }
            string Year { get; set; }
            string Rated { get; set; }
            string Released { get; set; }
            string Runtime { get; set; }
            string Genre { get; set; }
            string Director { get; set; }
            string Writer { get; set; }
            string Actors { get; set; }
            string Plot { get; set; }
            string Language { get; set; }
            string Country { get; set; }
            string Awards { get; set; }
            string Poster { get; set; }
            string Metascore { get; set; }
            string imdbRating { get; set; }
            string imdbVotes { get; set; }
            string imdbID { get; set; }
            string Type { get; set; }
            string Response { get; set; }
        }
    }
}
