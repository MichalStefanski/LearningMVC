using System.Collections.Generic;
using System.IO;
using MSConference.Domain.Abstract;
using MSConference.Domain.Entities;

namespace MSConference.Domain.Concrete
{
    //Guest Repository
    public class EFGuestRepository : IGuestRepository
    {
        private EfDbContext context1 = new EfDbContext();

        public IEnumerable<Guest> Guests
        {
            get { return context1.Guests; }
        }

        public void SaveGuest(Guest guest)
        {
            if (guest.GuestID == 0)
            {
                context1.Guests.Add(guest);
            }
            else
            {
                Guest dbEntry = context1.Guests.Find(guest.GuestID);
                if (dbEntry != null)
                {
                    dbEntry.GuestLastName = guest.GuestLastName;
                    dbEntry.GuestFirstName = guest.GuestFirstName;
                    dbEntry.GuestMiddleName = guest.GuestMiddleName;
                    dbEntry.GuestEmail = guest.GuestEmail;
                    dbEntry.GuestSex = guest.GuestSex;
                }
            }
            context1.SaveChanges();
        }        

        public Guest DeleteGuest(int guestId)
        {
            Guest dbEntry = context1.Guests.Find(guestId);
            if (dbEntry != null)
            {
                context1.Guests.Remove(dbEntry);
                context1.SaveChanges();
            }
            return dbEntry;
        }
    }

    //Contact Repository
    public class EFContactRepository : IContactRepository
    {
        private EfDbContext context2 = new EfDbContext();

        public IEnumerable<Contact> Contacts
        {
            get { return context2.Contacts; }
        }

        public void SaveContact(Contact contact, Guest guest)
        {            
            Contact dbEntry = context2.Contacts.Find(guest.GuestID);
            if (dbEntry == null)
            {
                Contact newContact = new Contact
                {
                    ContactID = guest.GuestID,
                    GuestID = guest.GuestID,
                    PostalCode = contact.PostalCode,
                    City = contact.City,
                    Street = contact.Street,
                    HouseNumber = contact.HouseNumber,
                    PhoneNumber = contact.PhoneNumber
                };                
                context2.Contacts.Add(newContact);
            }
            else if (dbEntry != null)
            {
                dbEntry.ContactID = contact.ContactID;
                dbEntry.GuestID = contact.GuestID;
                dbEntry.PostalCode = contact.PostalCode;
                dbEntry.City = contact.City;
                dbEntry.Street = contact.Street;
                dbEntry.HouseNumber = contact.HouseNumber;
                dbEntry.PhoneNumber = contact.PhoneNumber;
            }
            
        context2.SaveChanges();
        }        

        public Contact DeleteContact(int guestId)
        {
            Contact dbEntry = context2.Contacts.Find(guestId);
            if (dbEntry != null)
            {
                context2.Contacts.Remove(dbEntry);
                context2.SaveChanges();
            }
            return dbEntry;
        }
    }

    //QRCode Repository
    public class EFQRCodeRepository : IQRCodeRepository
    {
        private EfDbContext context3 = new EfDbContext();

        public IEnumerable<QRCode> QRCodes
        {
            get { return context3.QRCodes; }
        }

        public void CreateQRCode(Guest guest)
        {
            QRCode dbEntry = context3.QRCodes.Find(guest.GuestID);
            if (dbEntry == null)
            {
                QRCode qR = new QRCode();
                qR.GuestID = guest.GuestID;
                context3.QRCodes.Add(qR);
                context3.SaveChanges();
            }
        }

        public QRCode DeleteQRCode(int guestId)
        {
            QRCode dbEntry = context3.QRCodes.Find(guestId);
            if (dbEntry != null)
            {
                context3.QRCodes.Remove(dbEntry);
                context3.SaveChanges();
            }
            return dbEntry;
        }
    }       
}
