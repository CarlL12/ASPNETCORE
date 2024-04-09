using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Address { get; set; }

    public DbSet<SubscriberEntity> Subscribers { get; set; }

    public DbSet<CourseEntity> Courses { get; set; }

    public DbSet<CategoryEntity> Categories { get; set; }

    public DbSet<ContactEntity> Contacts { get; set; }  

    public DbSet<SavedCourseEntity> SavedCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
    }
}
