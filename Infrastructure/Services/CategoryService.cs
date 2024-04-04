
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
namespace Infrastructure.Services;

public class CategoryService(CategoryRepository repository)
{
    private readonly CategoryRepository _repository = repository;


    public async Task<IEnumerable<CategoryModel>> GetAllAsync()
    {
        List<CategoryModel> list = new List<CategoryModel>();

        CategoryModel model = new CategoryModel();

        var categories = await _repository.GetAllAsync();

        foreach(var category in categories)
        {
            model.CategoryName = category.CategoryName;
            model.Id = category.Id;
            list.Add(model);
        }

        return list;

    }
}
