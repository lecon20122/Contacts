using Microsoft.AspNetCore.Identity;

namespace Contacts.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<int>
{
}

