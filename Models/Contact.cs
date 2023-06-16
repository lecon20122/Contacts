using Contacts.Areas.Identity.Data;

namespace Contacts.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public ApplicationUser? User { get; set; }
        public int UserId { get; set; }

    }
}
