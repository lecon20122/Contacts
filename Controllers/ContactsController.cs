using Contacts.Areas.Identity.Data;
using Contacts.Business;
using Contacts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly ApplicationUserManager _userManager;

        public ContactsController(IContactRepository contactRepository, ApplicationUserManager userManager)
        {
            _contactRepository = contactRepository;
            _userManager = userManager;
        }

        // GET: Contacts
        public IActionResult Index()
        {
            var contacts = _contactRepository.GetContacts();
            return View(contacts);
        }

        // GET: Contacts/Details/5
        public IActionResult Details(int id)
        {
            Contact? contact = _contactRepository.GetContact(id);
            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = _userManager.UserId(User);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Email,Phone,Description,UserId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.AddContact(contact);
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = _userManager.UserId(User);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public IActionResult Edit(int id)
        {
            var contact = _contactRepository.GetContact(id);

            ViewData["UserId"] = _userManager.UserId(User);
            return View(contact);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Email,Phone,Description,UserId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contactRepository.EditContact(id, contact);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest("Somehting went wrong, try again later");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = _userManager.UserId(User);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public IActionResult Delete(int id)
        {
            var contact = _contactRepository.GetContact(id);

            if (contact is null) return NotFound();

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _contactRepository.DeleteContact(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
