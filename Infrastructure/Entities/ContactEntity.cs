

namespace Infrastructure.Entities;

public class ContactEntity
{
    public int id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Service { get; set; } = null!;

    public string Message { get; set; } = null!;
}
