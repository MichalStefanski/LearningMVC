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
}
