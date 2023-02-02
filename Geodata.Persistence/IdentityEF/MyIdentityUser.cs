using Microsoft.AspNetCore.Identity;

namespace Geodata.Persistence.IdentityEF;

public class MyIdentityUser : IdentityUser
{
    public string Name { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}

