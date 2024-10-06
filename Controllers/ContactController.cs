using ContactSystem.Models;
using ContactSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.FindAll();
            return View(contacts);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            await _contactService.Insert(contact);

            return RedirectToAction(nameof(Index));
        } 
    }
}
