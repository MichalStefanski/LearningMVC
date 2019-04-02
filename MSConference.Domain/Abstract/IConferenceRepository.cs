using System.Collections.Generic;
using MSConference.Domain.Entities;

namespace MSConference.Domain.Abstract
{
    public interface IGuestRepository
    {
        IEnumerable<Guest> Guests { get; }

        void SaveGuest(Guest guest);

        Guest DeleteGuest(int guestId);
    }

    public interface IContactRepository
    {
        IEnumerable<Contact> Contacts { get; }

        void SaveContact(Guest guest);

        Contact DeleteContact(int guestId);
    }

    public interface IQRCodeRepository
    {
        IEnumerable<QRCode> QRCodes { get; }

        void CreateQRCode(Guest guest);

        QRCode DeleteQRCode(int guestId);
    }
    //public interface IBillRepository
    //{
    //    IEnumerable<Bill> Bills { get; }
    //}    
}
