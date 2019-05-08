using MSConference.Domain.Entities;
using System.Data.Entity;

namespace MSConference.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("EfDbContext")
        {

        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Types().Configure(t => t.MapToStoredProcedures());
        //}

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<QRCode> QRCodes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentsView> PaymentsViews { get; set; }
    }
}
