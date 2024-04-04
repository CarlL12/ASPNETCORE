

namespace Infrastructure.Entities;
public class CourseEntity
{
    public int Id { get; set; }

    public string Image { get; set; } = null!;
    public string? BestSeller { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Price { get; set; }

    public string? SalePrice { get; set; }

    public string? OldPrice { get; set; }

    public string? Hours { get; set; }

    public string? Likes { get; set; }

    public int? categoryId { get; set; }

    public CategoryEntity? Category { get; set; }
}
