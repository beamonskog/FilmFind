namespace FilmFind.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }

        //[DataType(DataType.Password)]
        public string Password { get; set; }
        
        //[DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
