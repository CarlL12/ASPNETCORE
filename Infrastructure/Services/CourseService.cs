

using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CourseService(CourseRepository repository, SavedCourseRepository savedCourseRepository)
{
    private readonly CourseRepository _repository = repository;
    private readonly SavedCourseRepository savedCourseRepository = savedCourseRepository;

    public async Task<IEnumerable<CourseModel>> GetAllAsync(string category, string searchQuery)
    {
        List<CourseModel> list = new List<CourseModel>();


        var courses = await _repository.GetAllAsync(category, searchQuery);

        foreach (var course in courses)
        {
            CourseModel model = new CourseModel();

            model.Hours = course.Hours;
            model.Author = course.Author;
            model.OldPrice = course.OldPrice;
            model.SalePrice = course.SalePrice;
            model.Price = course.Price;
            model.BestSeller = course.BestSeller;
            model.Title = course.Title;
            model.Id = course.Id;
            model.Image = course.Image;
            model.Likes = course.Likes;
            model.Saved = course.Saved;
            model.Category = course.Category!.CategoryName;

            list.Add(model);
        }

        return list;
    }


    public async Task<bool> SaveOrDeleteCourse(int id)
    {
        var exists = await savedCourseRepository.ExistsAsync(x => x.Course == id);

        if(exists)
        {
            var courseResult = await _repository.GetOneAsync(x => x.Id == id);

            if(courseResult != null)
            {
                courseResult.Saved = false;

                var updateResult = await _repository.UpdateAsync(x => x.Id == id, courseResult);

                if(updateResult)
                {

                    var deleteResult = await savedCourseRepository.DeleteAsync(x => x.Course == id);

                    return false;
                }
            }

        }

        var result = await _repository.GetOneAsync(x => x.Id == id);

        if(result != null)
        {
            result.Saved = true;

            var updatedResult = await _repository.UpdateAsync(x => x.Id == id, result);

            SavedCourseEntity entity = new SavedCourseEntity
            {
                Course = id,
            };

            var createResult = await savedCourseRepository.CreateAsync(entity);

            return true;
        }

        return true;
    }

    public async Task<bool> DeleteSavedCourses()
    {
        var savedCourses = await savedCourseRepository.GetAllAsync();

        if(savedCourses != null)
        {
            foreach( var course in savedCourses)
            {
                var getOne = await _repository.GetOneAsync(x => x.Id == course.Course);

                getOne.Saved = false;

                var updateResult = await _repository.UpdateAsync(x => x.Id == getOne.Id, getOne);

                if(updateResult)
                {
                    var result = await savedCourseRepository.DeleteAsync(x => x.Course == getOne.Id);
                }
            }

            return true;
        }



        return false;
    }


    public async Task<IEnumerable<CourseModel>> GetAllSavedCourses()
    {
        List<CourseModel> list = new List<CourseModel>();

        var savedCourses = await savedCourseRepository.GetAllAsync();

        if(savedCourses != null)
        {
            foreach (var savedCourse in savedCourses)
            {
                var result = await _repository.GetOneAsync(x => x.Id == savedCourse.Course);

                CourseModel model = new CourseModel();

                model.Hours = result.Hours;
                model.Author = result.Author;
                model.OldPrice = result.OldPrice;
                model.SalePrice = result.SalePrice;
                model.Price = result.Price;
                model.BestSeller = result.BestSeller;
                model.Title = result.Title;
                model.Id = result.Id;
                model.Image = result.Image;
                model.Likes = result.Likes;
                model.Saved = result.Saved;

                list.Add(model);

            }

            return list;
        }

        return null!;

        
    }
}
