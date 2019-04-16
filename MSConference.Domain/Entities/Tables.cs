using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Web.Mvc;

namespace MSConference.Domain.Entities
{
    [Table("GuestsTest")]
    public class Guest
    {
        [Key]
        public int GuestID { get; set; }

        [Display(Name = "Nazwisko")]
        public string GuestLastName { get; set; }

        [Display(Name = "Piersze Imię")]
        public string GuestFirstName { get; set; }

        [Display(Name = "Drugie Imię")]
        public string GuestMiddleName { get; set; }

        [Display(Name = "Email")]
        public string GuestEmail { get; set; }

        [Display(Name = "Sex")]
        public string GuestSex { get; set; }

        //public virtual Contact Contact { get; set; }
        //public virtual QRCode QRCode { get; set; }
    }

    [Table("ContactsTest")]
    public class Contact
    {        
        public int ContactID { get; set; }
        [Key, ForeignKey("Guest")]
        public int GuestID { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Guest Guest { get; set; }
    }

    public class Plan
    {
        [Key]
        public int PrelectionID { get; set; }
        public string PrelectionTitle { get; set; }
        public char PrelectionDay { get; set; }
        public TimeSpan PrelectionStart { get; set; }
        public int PrelectionTime { get; set; }
        public int PrelectionCapacity { get; set; }
        public int PrelectionSitsTaken { get; set; }
    }

    [Table("QRCodesTest")]
    public class QRCode
    {
        [Key]
        public int QRID { get; set; }
        [ForeignKey("Guest")]
        public int GuestID { get; set; }
        public byte[] Code { get; set; }

        public virtual Guest Guest { get; set; }
    }    
}
