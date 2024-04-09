

using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ContactRepository(DataContext dataContext) : Repo<ContactEntity>(dataContext)
{
    private readonly DataContext _dataContext = dataContext;
}
