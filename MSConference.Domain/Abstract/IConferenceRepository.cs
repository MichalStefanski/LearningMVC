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

        void SaveContact(Contact contact, Guest guest);

        Contact DeleteContact(int guestId);
    }

    public interface IQRCodeRepository
    {
        IEnumerable<QRCode> QRCodes { get; }

        void CreateQRCode(Guest guest);

        QRCode DeleteQRCode(int guestId);
    }
    
    public interface IBookingRepository
    {
        IEnumerable<Booking> Bookings { get; }

        void SaveBooking(Booking booking, Guest guest);

        Booking DeleteBooking(int guestId);
    }

    public interface IPaymentRepository
    {
        IEnumerable<Payment> Payments { get; }

        void CreateBill(Guest guest);
        void CreateBill(int guest, Payment payment);

        Payment DeletePayment(int guestId);
    }

    public interface IPaymentsViewRepository
    {
        IEnumerable<PaymentsView> PaymentsViews { get; }
    }
}
