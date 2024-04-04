

using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CourseService(CourseRepository repository)
{
    private readonly CourseRepository _repository = repository;

    public async Task<IEnumerable<CourseModel>> GetAllAsync()
    {
        List<CourseModel> list = new List<CourseModel>(); 
        

        var courses = await _repository.GetAllAsync();

        foreach (var course in courses)
        {
            CourseModel model = new CourseModel();

            model.Hours = course.Hours;
            model.Author = course.Author;
            model.OldPrice = course.Price;
            model.SalePrice = course.SalePrice;
            model.Price = course.Price;
            model.BestSeller = course.BestSeller;
            model.Title = course.Title;
            model.Id = course.Id;
            model.Image = course.Image;
            model.Likes = course.Likes;
            model.Category = course.Category!.CategoryName;
            
            list.Add(model);
        }

        return list;
    }
}
