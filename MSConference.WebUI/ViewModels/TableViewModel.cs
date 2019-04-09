using MSConference.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MSConference.WebUI.ViewModels
{
    public class TableViewModel
    {
        public GuestViewModel GetGuest { get; set; }
        public ContactViewModel GetContact { get; set; }
        public PlanViewModel GetPlan { get; set; }
        public QRCodeViewModel GetQRCode { get; set; }
    }

    public class GuestViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int GuestID { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Proszę podać nazwisko.")]
        [Display(Name = "Nazwisko")]
        public string GuestLastName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Proszę podać imię.")]
        [Display(Name = "Imię")]
        public string GuestFirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Drugie imię")]
        public string GuestMiddleName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Proszę podać adres email.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Proszę podać prawidłowy adres e-mail.")]
        [Display(Name = "Email")]
        public string GuestEmail { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "Proszę podać płeć.")]
        public string GuestSex { get; set; }
        
    }

    public class ContactViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ContactID { get; set; }

        [Key, ForeignKey("Guest")]
        [HiddenInput(DisplayValue = false)]
        public int GuestID { get; set; }

        [Required(ErrorMessage = "Proszę podać kod pocztowy.")]
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Proszę podać Miejscowość.")]
        [Display(Name = "Miejscowość")]
        public string City { get; set; }

        [Required(ErrorMessage = "Proszę podać ulicę.")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Proszę podać numer domu/mieszkania.")]
        [Display(Name = "Numer domu/mieszkania")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Proszę podać numer telefonu.")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
    }

    public class PlanViewModel
    {  
        [Display(Name = "Temat")]
        public string PrelectionTitle { get; set; }

        [Display(Name = "Start prelekcji")]
        public TimeSpan PrelectionStart { get; set; }

        [Display(Name = "Czas trwania prelekcji")]
        public int PrelectionTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PrelectionCapacity { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PrelectionSitsTaken { get; set; }
    }

    public class QRCodeViewModel
    {
        [Key, ForeignKey("Guest")]
        public int GuestID { get; set; }
        public byte[] Code { get; set; }
    }
}