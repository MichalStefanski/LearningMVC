using System.Web.Mvc;
using MSConference.WebUI.Infrastructure.Abstract;
using MSConference.WebUI.Models;

namespace MSConference.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.LoginMail, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowy adres mailowy użytkownika lub niepoprawne hasło.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}