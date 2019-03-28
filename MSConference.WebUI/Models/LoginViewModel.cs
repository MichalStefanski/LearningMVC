using System.ComponentModel.DataAnnotations;

namespace MSConference.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Proszę podać email użytkownika.")]
        public string LoginMail { get; set; }
        [Required(ErrorMessage = "Proszę podać hasło.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
    }
}