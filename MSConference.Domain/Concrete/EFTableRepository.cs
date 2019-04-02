using System.Collections.Generic;
using MSConference.Domain.Abstract;
using MSConference.Domain.Entities;

namespace MSConference.Domain.Concrete
{
//Guest Repository
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
                    dbEntry.GuestSex = guest.GuestSex;
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

    //Contact Repository
    public class EFContactRepository : IContactRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<Contact> Contacts
        {
            get { return context.Contacts; }
        }

        public void SaveContact(Guest guest)
        {
            Contact contact = new Contact();
            if (guest.GuestID == 0)
            {
                contact.GuestID = guest.GuestID;
                context.Contacts.Add(contact);
            }
            else
            {
                Contact dbEntry = context.Contacts.Find(guest.GuestID);
                if (dbEntry != null)
                {
                    dbEntry.PostalCode = contact.PostalCode;
                    dbEntry.City = contact.City;
                    dbEntry.Street = contact.Street;
                    dbEntry.HouseNumber = contact.HouseNumber;
                    dbEntry.PhoneNumber = contact.PhoneNumber;
                }
            }
            context.SaveChanges();
        }

        public Contact DeleteContact(int guestId)
        {
            Contact dbEntry = context.Contacts.Find(guestId);
            if (dbEntry != null)
            {
                context.Contacts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }

    //QRCode Repository
    public class EFQRCodeRepository : IQRCodeRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<QRCode> QRCodes
        {
            get { return context.QRCodes; }
        }

        public void CreateQRCode(Guest guest)
        {
            QRCode dbEntry = context.QRCodes.Find(guest.GuestID);
            if (dbEntry == null)
            {
                QRCode qR = new QRCode();
                qR.GuestID = guest.GuestID;
                context.QRCodes.Add(qR);
                context.SaveChanges();
            }
        }

        public QRCode DeleteQRCode(int guestId)
        {
            QRCode dbEntry = context.QRCodes.Find(guestId);
            if (dbEntry != null)
            {
                context.QRCodes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
