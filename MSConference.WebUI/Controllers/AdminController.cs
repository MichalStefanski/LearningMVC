using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MSConference.Domain.Abstract;
using MSConference.Domain.Concrete;
using MSConference.Domain.Entities;

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
            EfDbContext ef = new EfDbContext();
            List<Guest> MyGuests = ef.Guests.ToList();
            return View(MyGuests);
        }

        public PartialViewResult Menu()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult EditGuest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = guestRepository.Guests.FirstOrDefault(p => p.GuestID == id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        [HttpPost]
        public ActionResult EditGuest(Guest guest)
        {
            if (ModelState.IsValid)
            {
                
                guestRepository.SaveGuest(guest);
                qrcodeRepository.CreateQRCode(guest);
                TempData["message"] = string.Format("Zapisano {0} {1}", guest.GuestFirstName, guest.GuestLastName);
                return RedirectToAction("EditGuest/" + guest.GuestID);
            }
            else
            {
                return View(guest);
            }
        }

        [HttpGet]
        public ActionResult EditContact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactRepository.Contacts.FirstOrDefault(p => p.GuestID == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HttpPost]
        public ActionResult EditContact(Contact contact, Guest guest)
        {
            if (ModelState.IsValid)
            {
                contactRepository.SaveContact(contact);
                TempData["message"] = string.Format("Zapisano {0} {1}", guest.GuestFirstName, guest.GuestLastName);
                return RedirectToAction("EditGuest/" + contact.GuestID);
            }
            else
            {
                return View(contact);
            }
        }

        public ActionResult Create()
        {
            return View("EditGuest", new Guest());
        }

        [HttpPost]
        public ActionResult Delete(int guestId)
        {            
            QRCode deleteQRCode = qrcodeRepository.DeleteQRCode(guestId);
            Guest deletedGuest = guestRepository.DeleteGuest(guestId);
            if (deletedGuest != null)
            {
                TempData["message"] = string.Format("Usunięto {0} {1}", deletedGuest.GuestFirstName, deletedGuest.GuestLastName);
            }
            return RedirectToAction("Index");
        }
    }
}