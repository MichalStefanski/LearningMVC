using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MSConference.Domain.Abstract;
using MSConference.Domain.Concrete;
using MSConference.Domain.Entities;
using MSConference.WebUI.ViewModels;

namespace MSConference.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IGuestRepository guestRepository;
        private IContactRepository contactRepository;
        private IQRCodeRepository qrcodeRepository;
        private IBookingRepository bookingRepository;
        private IPaymentRepository paymentRepository;
        private IPaymentsViewRepository paymentsViewRepository;
         
        public AdminController(IGuestRepository repoG, IContactRepository repoC, IQRCodeRepository repoQ, IBookingRepository repoB, IPaymentRepository repoP, IPaymentsViewRepository repoV)
        {
            guestRepository = repoG;
            contactRepository = repoC;
            qrcodeRepository = repoQ;
            bookingRepository = repoB;
            paymentRepository = repoP;
            paymentsViewRepository = repoV;
        }

        public ActionResult Index()
        {            
            return View(guestRepository.Guests);
        }

        public ActionResult Bills()
        {
            return View(paymentsViewRepository.PaymentsViews);
        }
        
        public ActionResult EditGuest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Guest guest = guestRepository.Guests.FirstOrDefault(g => g.GuestID == id);
            Contact contact = contactRepository.Contacts.FirstOrDefault(c => c.ContactID == id);
            Booking booking = bookingRepository.Bookings.FirstOrDefault(b => b.BookingID == id);

            if (guest == null)
            {
                return HttpNotFound();
            }

            TableViewModel tableView = new TableViewModel
            {
                GetGuest = new GuestViewModel
                {
                    GuestID = guest.GuestID,
                    GuestLastName = guest.GuestLastName,
                    GuestFirstName = guest.GuestFirstName,
                    GuestMiddleName = guest.GuestMiddleName,
                    GuestEmail = guest.GuestEmail,
                    GuestSex = guest.GuestSex
                },

                GetContact = new ContactViewModel
                {
                    GuestID = guest.GuestID,
                    ContactID = contact.ContactID,
                    PostalCode = contact.PostalCode,
                    City = contact.City,
                    Street = contact.Street,
                    HouseNumber = contact.HouseNumber,
                    PhoneNumber = contact.PhoneNumber
                },

                GetBooking = new BookingViewModel
                {
                    GuestID = guest.GuestID,
                    BookingID = booking.BookingID,
                    RoomType = booking.RoomType,
                    Banquet = booking.Banquet,
                    Activity = booking.Activity
                }
            };      

            return View(tableView);
        }
           
        [HttpPost]
        public ActionResult EditGuest(TableViewModel _model)
        {            
            if (ModelState.IsValid)
            {
                Guest guest = new Guest
                {
                    GuestID = _model.GetGuest.GuestID,
                    GuestLastName = _model.GetGuest.GuestLastName,
                    GuestFirstName = _model.GetGuest.GuestFirstName,
                    GuestMiddleName = _model.GetGuest.GuestMiddleName,
                    GuestEmail = _model.GetGuest.GuestEmail,
                    GuestSex = _model.GetGuest.GuestSex
                };
                Contact contact = new Contact
                {
                    ContactID = _model.GetContact.ContactID,
                    GuestID = _model.GetContact.GuestID,
                    PostalCode = _model.GetContact.PostalCode,
                    City = _model.GetContact.City,
                    Street = _model.GetContact.Street,
                    HouseNumber = _model.GetContact.HouseNumber,
                    PhoneNumber = _model.GetContact.PhoneNumber
                };
                Booking booking = new Booking
                {
                    BookingID = _model.GetBooking.BookingID,
                    GuestID = _model.GetBooking.GuestID,
                    RoomType = _model.GetBooking.RoomType,
                    Banquet = _model.GetBooking.Banquet,
                    Activity = _model.GetBooking.Activity
                };
                
                guestRepository.SaveGuest(guest);
                qrcodeRepository.CreateQRCode(guest);
                contactRepository.SaveContact(contact, guest);
                bookingRepository.SaveBooking(booking, guest);
                paymentRepository.CreateBill(guest);

                TempData["message"] = string.Format("Zapisano {0} {1}", _model.GetGuest.GuestFirstName, _model.GetGuest.GuestLastName);
                return RedirectToAction("EditGuest/" + guest.GuestID);
            }
            else
            {
                return View("EditGuest", _model);
            }
        }       

        public ActionResult Create()
        {
            return View("EditGuest", new TableViewModel()
            {
                GetBooking = new BookingViewModel(),
                GetContact = new ContactViewModel(),
                GetGuest = new GuestViewModel()        
            });
        }

        public ActionResult EditPayment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Guest guest = guestRepository.Guests.FirstOrDefault(g => g.GuestID == id);
            Booking booking = bookingRepository.Bookings.FirstOrDefault(b => b.BookingID == id);
            Payment payment = paymentRepository.Payments.FirstOrDefault(p => p.GuestID == id);

            if (payment == null)
            {
                return HttpNotFound();
            }

            TableViewModel tableView = new TableViewModel
            {
                GetGuest = new GuestViewModel
                {
                    GuestID = guest.GuestID,
                    GuestLastName = guest.GuestLastName,
                    GuestFirstName = guest.GuestFirstName,
                    GuestMiddleName = guest.GuestMiddleName,
                    GuestEmail = guest.GuestEmail,
                    GuestSex = guest.GuestSex
                },

                GetBooking = new BookingViewModel
                {
                    GuestID = guest.GuestID,
                    BookingID = booking.BookingID,
                    RoomType = booking.RoomType,
                    Banquet = booking.Banquet,
                    Activity = booking.Activity
                },

                GetPayment = new PaymentViewModel
                {
                    PaymentID = payment.PaymentID,
                    GuestID = payment.GuestID,
                    BillValue = payment.BillValue,
                    PaidValue = payment.PaidValue,
                    DateOfPayment = payment.DateOfPayment,
                    DateToBill = payment.DateToBill,
                    BankInfo = payment.BankInfo,
                    AccountInfo = payment.AccountInfo,
                    IsPaidFull = payment.IsPaidFull,
                    AmmountLeft = payment.AmmountLeft,
                    Notes = payment.Notes
                }
            };

            return View(tableView);
        }

        [HttpPost]
        public ActionResult EditPayment(TableViewModel _model)
        {
            if (ModelState.IsValid)
            {                   
                Payment payment = new Payment
                {
                    PaymentID = _model.GetPayment.PaymentID,
                    GuestID = _model.GetPayment.GuestID,
                    BillValue = _model.GetPayment.BillValue,
                    PaidValue = _model.GetPayment.PaidValue,
                    DateOfPayment = _model.GetPayment.DateOfPayment,
                    DateToBill = _model.GetPayment.DateToBill,
                    BankInfo = _model.GetPayment.BankInfo,
                    AccountInfo = _model.GetPayment.AccountInfo,
                    IsPaidFull = _model.GetPayment.IsPaidFull,
                    AmmountLeft = _model.GetPayment.AmmountLeft,
                    Notes = _model.GetPayment.Notes
                };

                paymentRepository.CreateBill(_model.GetPayment.GuestID, payment);

                TempData["message"] = string.Format("Zapisano płatność");
                return View("Bills", paymentsViewRepository.PaymentsViews);
            }
            else
            {
                return View("EditPayment", _model);
            }
        }

        [HttpPost]
        public ActionResult Delete(int guestId)
        {
            Booking deleteBooking = bookingRepository.DeleteBooking(guestId);
            Contact deletedContact = contactRepository.DeleteContact(guestId);
            QRCode deletedQRCode = qrcodeRepository.DeleteQRCode(guestId);
            Guest deletedGuest = guestRepository.DeleteGuest(guestId);
            if (deletedGuest != null)
            {
                TempData["message"] = string.Format("Usunięto {0} {1}", deletedGuest.GuestFirstName, deletedGuest.GuestLastName);
            }
            else
            {
                TempData["message"] = string.Format("Nie można usunąć {0} {1}", deletedGuest.GuestFirstName, deletedGuest.GuestLastName);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ErasePayment (int guestId)
        {
            Payment deletePayment = paymentRepository.DeletePayment(guestId);
            return RedirectToAction("Bills");
        }

        [HttpPost]
        public JsonResult DuplicateGuest(TableViewModel _model)
        {            
            var isEqual = guestRepository.Guests.Any(x => x.GuestEmail == _model.GetGuest.GuestEmail);
            
            if (_model.GetGuest.GuestID == 0)
            {
                return Json(!isEqual, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var isTheSame = guestRepository.Guests.FirstOrDefault(x => x.GuestID == _model.GetGuest.GuestID).GuestEmail;
                if (isTheSame == _model.GetGuest.GuestEmail)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                return Json(!isEqual, JsonRequestBehavior.AllowGet);
            }
                                   
        }
    }
}