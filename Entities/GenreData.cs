using System;

namespace FilmFind.Entities
{
    public class GenreData
    {
        public int Id { get; set; }
        public Genre Genre { get; set; }

        public static Genre StringToGenre(string input)
        {
            var modifiedInput = input.Trim().ToLower();
            if (modifiedInput.Contains("sci"))
            {
                return Genre.SciFi;
            }
            modifiedInput = Char.ToUpper(modifiedInput[0]) + modifiedInput.Remove(0, 1);

            Genre thisGenre;
            if (!Enum.TryParse(modifiedInput, out thisGenre))
                throw new Exception($"Unable to find {modifiedInput} among existing genres!");
            return thisGenre;
        }
    }

    public enum Genre
    {
        Drama,
        Crime,
        Thriller,
        Action,
        Adventure,
        SciFi,
        Animation,
        Horror,
        Comedy,
        Fantasy,
        Biography,
        Sport,
        Family,
        Documentary,
        Romance
    }

}
