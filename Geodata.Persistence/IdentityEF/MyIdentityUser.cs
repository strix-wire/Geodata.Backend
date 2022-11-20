using Microsoft.AspNetCore.Identity;

namespace Geodata.Persistence.IdentityEF;

public class MyIdentityUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? MiddleName { get; set; }
    public string? DateOfBirth { get; set; }
    public string? City { get; set; }
    public string? Sex { get; set; }
}

