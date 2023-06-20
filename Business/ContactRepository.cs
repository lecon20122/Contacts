using Contacts.Areas.Identity.Data;
using Contacts.Data;
using Contacts.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Business
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContext;

        private int UserId => _userManager.UserId(_httpContext.HttpContext.User);

        public ContactRepository(ApplicationDbContext context, ApplicationUserManager userManager, IHttpContextAccessor httpContext)
        {
            _context = context;
            _userManager = userManager;
            _httpContext = httpContext;
        }

        public void AddContact(Contact contact)
        {
            _context.Add(contact);
            _context.SaveChanges();
        }

        public void DeleteContact(int id)
        {
            var contact = GetContact(id);

            if (contact is null) throw new Exception("Item Not Found");

            _context.Contact.Remove(contact);
            _context.SaveChanges();

        }

        public Contact? GetContact(int id)
        {
            return _context.Contact?
                .Where(c => c.UserId == UserId)
                .FirstOrDefault(m => m.Id == id);
        }

        public List<Contact> GetContacts()
        {
            return _context.Contact.Where(c => c.UserId == UserId)
             .Include(c => c.User)
             .ToList();
        }

        public void EditContact(int id, Contact contact)
        {
            if (id != contact.Id || !ContactExists(contact.Id)) throw new Exception("Item Not Found");

            _context.Update(contact);
            _context.SaveChanges();
        }

        public bool ContactExists(int id)
        {
            return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
