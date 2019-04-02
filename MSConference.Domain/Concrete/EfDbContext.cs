using MSConference.Domain.Entities;
using System.Data.Entity;

namespace MSConference.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<QRCode> QRCodes { get; set; }
    }
}
