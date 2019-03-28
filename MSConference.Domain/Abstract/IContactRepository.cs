using System.Collections.Generic;
using MSConference.Domain.Entities;

namespace MSConference.Domain.Abstract
{
    public interface IContactRepository
    {
        IEnumerable<Contact> Contacts { get; }

        void SaveContact (Contact contact);

        Contact DeleteContact(int guestId);
    }
}
