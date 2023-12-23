namespace Geodata.Domain;

public class GeodataDomain
{
    public int T { get; set; }
    public int Y { get; set; }
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
    public string Title { get; set; }
    public string? Details { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    /// <summary>
    /// Checked by moderator
    /// </summary>
    public bool IsChecked { get; set; }
}
