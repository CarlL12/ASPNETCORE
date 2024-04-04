
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CourseRepository(DataContext context) : Repo<CourseEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<CourseEntity>> GetAllAsync()
    {
        var query = _context.Courses.Include(i => i.Category).AsQueryable();

        query = query.OrderByDescending(o => o.Id);

        var courses = await query.ToListAsync();

        return courses;
    }
}

