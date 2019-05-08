using MSConference.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using MSConference.WebUI.Models;

namespace MSConference.WebUI.ViewModels
{
    public class TableViewModel
    {
        public GuestViewModel GetGuest { get; set; }
        public ContactViewModel GetContact { get; set; }
        public PlanViewModel GetPlan { get; set; }
        public BookingViewModel GetBooking { get; set; }
        public QRCodeViewModel GetQRCode { get; set; }
        public PaymentViewModel GetPayment { get; set; }
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
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Proszę podać adres email.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Proszę podać prawidłowy adres e-mail.")]
        [Remote("DuplicateGuest", "Admin", AdditionalFields = "GuestID", HttpMethod = "Post", ErrorMessage = "Ten adres Email jest już zajęty")]        
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

    public class BookingViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int BookingID { get; set; }

        [Key, ForeignKey("Guest")]
        [HiddenInput(DisplayValue = false)]        
        public int GuestID { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z poniższych opcji.")]
        [Display(Name = "Forma zakwaterowania")]
        public int RoomType { get; set; }

        [Required(ErrorMessage = "Proszę wybrać Tak lub Nie")]
        [Display(Name = "Uczestnictwo w bankiecie")]
        public bool Banquet { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z ścieżek.")]
        [Display(Name = "Ścieżka tematyczna")]
        public int Activity { get; set; }
    }

    public class QRCodeViewModel
    {
        [Key, ForeignKey("Guest")]
        public int GuestID { get; set; }
        public byte[] Code { get; set; }
    }

    public class PaymentViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PaymentID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Key, ForeignKey("Guest")]
        public int GuestID { get; set; }

        [Display(Name = "Kwota rachunku")]
        public decimal BillValue { get; set; }

        [Display(Name = "Wpłata")]
        public decimal PaidValue { get; set; }

        [Display(Name = "Data zapłaty")]
        public DateTime? DateOfPayment { get; set; }

        [Required(ErrorMessage = "Wprowadź ostateczny termin zapłaty")]
        [Display(Name = "Termin płatności")]
        public DateTime DateToBill { get; set; }

        [Display(Name = "PIerwsze 8 cyfr BNR")]
        public string BankInfo { get; set; }

        [Display(Name = "Ostatnie 4 cyfry BNR")]
        public string AccountInfo { get; set; }

        [Display(Name = "Status Płatności")]
        public int IsPaidFull { get; set; }

        [Display(Name = "Pozostała kwota do zapłaty")]
        public decimal AmmountLeft { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Notatki")]
        public string Notes { get; set; }
    }    
}