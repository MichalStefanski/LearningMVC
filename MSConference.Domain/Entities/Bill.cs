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
    public class Bill
    {
        [Key]
        public int GuestID { get; set; }
        public int RoomType { get; set; }
        public bool OnBanquet { get; set; }
        public bool DiscountTime { get; set; }
        public bool DiscountCash { get; set; }
    }
}
