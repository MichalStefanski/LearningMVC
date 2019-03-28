using System.Collections.Generic;
using MSConference.Domain.Abstract;
using MSConference.Domain.Entities;

namespace MSConference.Domain.Concrete 
{
    public class EFGuestRepository : IGuestRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<Guest> Guests
        {
            get { return context.Guests; }
        }

        public void SaveGuest(Guest guest)
        {
            if (guest.GuestID == 0)
            {
                context.Guests.Add(guest);
            }
            else
            {
                Guest dbEntry = context.Guests.Find(guest.GuestID);
                if (dbEntry != null)
                {
                    dbEntry.GuestLastName = guest.GuestLastName;
                    dbEntry.GuestFirstName = guest.GuestFirstName;
                    dbEntry.GuestMiddleName = guest.GuestMiddleName;
                    dbEntry.GuestEmail = guest.GuestEmail;
                }
            }
            context.SaveChanges();
        }

        public Guest DeleteGuest(int guestId)
        {
            Guest dbEntry = context.Guests.Find(guestId);
            if (dbEntry != null)
            {
                context.Guests.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
