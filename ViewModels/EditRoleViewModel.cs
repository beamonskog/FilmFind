using System.ComponentModel.DataAnnotations;

namespace FilmFind.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        [Required, MinLength(3)]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }

}
