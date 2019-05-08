using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                EfDbContext efDb = new EfDbContext();
                SqlParameter paramId = new SqlParameter("@GID", guest.GuestID);
                int result = efDb.Database.ExecuteSqlCommand("SP_CreateQRCode @GID", paramId);
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

    public class EFBookingRepository : IBookingRepository
    {
        private EfDbContext context4 = new EfDbContext();

        public IEnumerable<Booking> Bookings
        {
            get { return context4.Bookings; }
        }

        public void SaveBooking(Booking booking, Guest guest)
        {
            Booking dbEntry = context4.Bookings.Find(guest.GuestID);
            if (dbEntry == null)
            {
                Booking newBooking = new Booking
                {
                    BookingID = guest.GuestID,
                    GuestID = guest.GuestID,
                    RoomType = booking.RoomType,
                    Banquet = booking.Banquet,
                    Activity = booking.Activity
                };
                context4.Bookings.Add(newBooking);
            }
            else if (dbEntry != null)
            {
                dbEntry.BookingID = booking.BookingID;
                dbEntry.GuestID = booking.GuestID;
                dbEntry.RoomType = booking.RoomType;
                dbEntry.Banquet = booking.Banquet;
                dbEntry.Activity = booking.Activity;
            }

            //EfDbContext efDb = new EfDbContext();
            //int prep = efDb.Database.ExecuteSqlCommand("SP_ClearRooms");
            //foreach (var item in context4.Bookings)
            //{
            //    SqlParameter paramId = new SqlParameter("@GID", item.GuestID);
            //    int result = efDb.Database.ExecuteSqlCommand("SP_AssignPair @GID", paramId);
            //}            

            context4.SaveChanges();
        }

        public Booking DeleteBooking(int guestId)
        {
            Booking dbEntry = context4.Bookings.Find(guestId);
            if (dbEntry != null)
            {
                EfDbContext efDb = new EfDbContext();
                int prep = efDb.Database.ExecuteSqlCommand("SP_ClearRooms");
                
                context4.Bookings.Remove(dbEntry);
                context4.SaveChanges();
            }
            return dbEntry;
        }
    }

    public class EFPaymentRepository : IPaymentRepository
    {
        private EfDbContext context5 = new EfDbContext();

        public IEnumerable<Payment> Payments
        {
            get { return context5.Payments; }
        }

        public void CreateBill(Guest guest)
        {
            string TimeToPay = "2019-05-12";
            Payment dbEntry = context5.Payments.Find(guest.GuestID);
            if (dbEntry == null)
            {
                Payment newPayment = new Payment()
                {
                    PaymentID = guest.GuestID,
                    GuestID = guest.GuestID,
                    PaidValue = 0,
                    DateToBill = Convert.ToDateTime(TimeToPay),
                    Notes = "Uzupełnić dane z przelewu"
                };
                context5.Payments.Add(newPayment);
            }
        }

        public void CreateBill(int guest, Payment payment)
        {
            Payment dbEntry = context5.Payments.Find(guest);
            if (dbEntry != null)
            {
                dbEntry.PaymentID = payment.PaymentID;
                dbEntry.GuestID = payment.GuestID;
                dbEntry.PaidValue = payment.PaidValue;
                dbEntry.DateOfPayment = payment.DateOfPayment;
                dbEntry.BankInfo = payment.BankInfo;
                dbEntry.AccountInfo = payment.AccountInfo;
                dbEntry.Notes = payment.Notes;
            }
            context5.SaveChanges();
        }

        public Payment DeletePayment(int guestId)
        {
            Payment dbEntry = context5.Payments.Find(guestId);
            if (dbEntry != null & dbEntry.PaidValue==0)
            {
                context5.Payments.Remove(dbEntry);
                context5.SaveChanges();
            }
            return dbEntry;
        }
    }

    public class EFPaymentsViewRepository : IPaymentsViewRepository
    {
        private EfDbContext context6 = new EfDbContext();

        public IEnumerable<PaymentsView> PaymentsViews
        {
            get { return context6.PaymentsViews; }
        }
    }
}
