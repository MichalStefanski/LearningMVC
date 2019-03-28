using System.Collections.Generic;
using MSConference.Domain.Abstract;
using MSConference.Domain.Entities;

namespace MSConference.Domain.Concrete
{
    public class EFContactRepository : IContactRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<Contact> Contacts
        {
            get { return context.Contacts; }
        }

        public void SaveContact(Contact contact)
        {
            if (contact.GuestID == 0)
            {
                context.Contacts.Add(contact);
            }
            else
            {
                Contact dbEntry = context.Contacts.Find(contact.GuestID);
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

        public Contact DeleteContact (int guestId)
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
}
