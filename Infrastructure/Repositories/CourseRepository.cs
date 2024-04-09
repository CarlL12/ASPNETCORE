
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CourseRepository(DataContext context) : Repo<CourseEntity>(context)
{
    private readonly DataContext _context = context;

    public async Task<IEnumerable<CourseEntity>> GetAllAsync(string category, string searchQuery)
    {
        var query = _context.Courses.Include(i => i.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(category) && category != "all")
            query = query.Where(x => x.Category!.CategoryName == category);

        if (!string.IsNullOrEmpty(searchQuery))
            query = query.Where(x => x.Title!.Contains(searchQuery) || x.Author!.Contains(searchQuery));

        query = query.OrderByDescending(o => o.Id);


        var courses = await query.ToListAsync();

        return courses;
    }
}

