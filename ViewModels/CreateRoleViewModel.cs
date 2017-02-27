using System.ComponentModel.DataAnnotations;

namespace FilmFind.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required, MinLength(3)]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
