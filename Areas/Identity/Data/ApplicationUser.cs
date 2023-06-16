using Contacts.Models;
using Microsoft.AspNetCore.Identity;

namespace Contacts.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<int>
{
    public ApplicationUser()
    {
        Contacts = new List<Contact>();
    }
    public List<Contact> Contacts { get; set; }
}

