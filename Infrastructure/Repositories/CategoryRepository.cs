
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository(DataContext context) : Repo<CategoryEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<CategoryEntity>> GetAllAsync()
    {

        var categories = await _context.Categories.OrderBy(o  => o.CategoryName).ToListAsync();

        return categories;


    }
}
