using Microsoft.AspNetCore.Identity;

namespace API.Models;

// A.1 This type is our "User profile card" stored in the Identity system.
// - IdentityUser gives us the standard login fields (Email, PasswordHash, PhoneNumber, etc.).
// - We inherit so contributor profile data lives in the SAME table (AspNetUsers) as the auth data.
//   This keeps the beginner project simpler (fewer tables/joins) while still being realistic.
//
// Where:
// - Identity is configured in Program.cs (AddIdentityApiEndpoints<ApplicationUser>).
// - Admin provisioning: API/Endpoints/AdminUserEndpoints.cs (POST /api/admin/users).
// - Contributor self-service: API/Endpoints/ContributorEndpoints.cs (GET /api/contributor/me).
// - Frontend display: #/portal/me reads these fields via AuthContext.refreshMe().
//
// B.1 Domain rule tie-in (Scheduler-Radio): Contributors are the people who host programs.
//     The UI will later show this data on the "My Contributor" page.
public class ApplicationUser : IdentityUser
{
    // B.2 Address is part of the contributor profile (future: used for invoicing/payments).
    public string? Address { get; set; }

    // B.3 Optional promotional text that can be shown on a public contributor page.
    public string? Bio { get; set; }

    // B.4 Optional profile image URL (kept as a URL so we avoid file uploads in this iteration).
    public string? PhotoUrl { get; set; }

    // B.5 Business rule: users created by an admin start with a temporary password.
    //     When true, we will later force the user to change their password before using the app.
    public bool MustChangePassword { get; set; }
}
