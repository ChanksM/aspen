using Microsoft.AspNetCore.Mvc;
using MVCWithFakeRepository.Models;
using PartyInvites.Models;
using System.Diagnostics;
using System.Linq;

namespace MVCWithFakeRepository.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View("MyView");
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                Repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }
        }

        public ViewResult ListResponses()
        {
            return View(Repository.Responses.Where(r => r.WillAttend == true));
        }

    }
}
