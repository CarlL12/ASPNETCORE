

using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class SavedCourseRepository(DataContext context) : Repo<SavedCourseEntity>(context)
{
    public readonly DataContext context = context;
}
