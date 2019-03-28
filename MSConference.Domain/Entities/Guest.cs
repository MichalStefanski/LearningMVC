using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MSConference.Domain.Entities
{
    //[Table("Guests")]
    public class Guest
    {
        [HiddenInput(DisplayValue = false)]
        public int GuestID { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwisko.")]
        [Display(Name = "Nazwisko")]
        public string GuestLastName { get; set; }

        [Required(ErrorMessage = "Proszę podać imię.")]
        [Display(Name = "Imię")]
        public string GuestFirstName { get; set; }
                
        [Display(Name = "Drugie imię")]
        public string GuestMiddleName { get; set; }

        [Required(ErrorMessage = "Proszę podać adres email.")]
        [Display(Name = "Email")]
        public string GuestEmail { get; set; }
    }    
}
