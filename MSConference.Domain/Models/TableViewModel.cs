using MSConference.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSConference.Domain.Models
{
    public class TableViewModel
    {
        public GuestViewModel GetGuest { get; set; }
        public ContactViewModel GetContact { get; set; }
    }

    public class GuestViewModel
    {        
        [Key]
        public int GuestID { get; set; }
        public string GuestLastName { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestMiddleName { get; set; }
        public string GuestEmail { get; set; }
        public string GuestSex { get; set; }

        [Required]
        public virtual Guest Guest { get; set; }
    }

    public class ContactViewModel
    {
        [Key]     
        public int ContactID { get; set; }
        public int GuestID { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public virtual Contact Contact { get; set; }
    }
}