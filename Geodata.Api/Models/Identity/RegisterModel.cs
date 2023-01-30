namespace Geodata.Api.Models.Identity
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string? MiddleName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? City { get; set; }
        public string? Sex { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
