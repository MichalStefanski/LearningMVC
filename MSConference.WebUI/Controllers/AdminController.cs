using System.Linq;
using System.Net;
using System.Web.Mvc;
using MSConference.Domain.Abstract;
using MSConference.Domain.Entities;

namespace MSConference.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IGuestRepository guestRepository;

        public AdminController(IGuestRepository repoG)
        {
            guestRepository = repoG;
        }

        public ActionResult Index()
        {
            return View(guestRepository.Guests);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
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
        public ActionResult Edit(Guest guest)
        {
            if (ModelState.IsValid)
            {
                guestRepository.SaveGuest(guest);
                TempData["message"] = string.Format("Zapisano {0} {1}", guest.GuestFirstName, guest.GuestLastName);
                return RedirectToAction("Index");
            }
            else
            {
                return View(guest);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Guest());
        }

        [HttpPost]
        public ActionResult Delete(int guestId)
        {
            Guest deletedGuest = guestRepository.DeleteGuest(guestId);
            if (deletedGuest != null)
            {
                TempData["message"] = string.Format("Usunięto {0} {1}", deletedGuest.GuestFirstName, deletedGuest.GuestLastName);
            }
            return RedirectToAction("Index");
        }
    }
}