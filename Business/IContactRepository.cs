using Contacts.Models;

namespace Contacts.Business
{
    public interface IContactRepository
    {
        public Contact? GetContact(int id);

        public List<Contact> GetContacts();

        public void DeleteContact(int id);

        public void EditContact(int id, Contact contact);

        public void AddContact(Contact contact);

        public bool ContactExists(int id);
    }
}
