using System.Collections.Generic;
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
         
        public AdminController(IGuestRepository repoG, IContactRepository repoC, IQRCodeRepository repoQ)
        {
            guestRepository = repoG;
            contactRepository = repoC;
            qrcodeRepository = repoQ;
        }

        public ActionResult Index()
        {            
            return View(guestRepository.Guests);
        }
        
        public ActionResult EditGuest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Guest guest = guestRepository.Guests.FirstOrDefault(g => g.GuestID == id);
            Contact contact = contactRepository.Contacts.FirstOrDefault(c => c.ContactID == id);

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

                guestRepository.SaveGuest(guest);
                qrcodeRepository.CreateQRCode(guest);
                contactRepository.SaveContact(contact, guest);

                TempData["message"] = string.Format("Zapisano {0} {1}", _model.GetGuest.GuestFirstName, _model.GetGuest.GuestLastName);
                return RedirectToAction("EditGuest/" + guest.GuestID);
            }
            else
            {
                return View("Index");
            }
        }       

        public ActionResult Create()
        {
            return View("EditGuest", new TableViewModel()
            {
                GetContact = new ContactViewModel(),
                GetGuest = new GuestViewModel()
            });
        }        

        [HttpPost]
        public ActionResult Delete(int guestId)
        {
            Contact deletedContact = contactRepository.DeleteContact(guestId);
            QRCode deletedQRCode = qrcodeRepository.DeleteQRCode(guestId);
            Guest deletedGuest = guestRepository.DeleteGuest(guestId);
            if (deletedGuest != null)
            {
                TempData["message"] = string.Format("Usunięto {0} {1}", deletedGuest.GuestFirstName, deletedGuest.GuestLastName);
            }
            return RedirectToAction("Index");
        }
    }
}