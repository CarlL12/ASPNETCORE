using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/Courses")]
[ApiController]
[UseApiKey]
public class CourseController(CourseRepository courseRepository, CourseService courserService) : ControllerBase
{

    private readonly CourseRepository _courseRepository = courseRepository;
    private readonly CourseService _courseService = courserService;


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CourseModel model)
    {
        if (ModelState.IsValid)
        {
            CourseEntity Entity = new CourseEntity
            {
                Image = model.Image,
                BestSeller = model.BestSeller,
                Title = model.Title,
                Author = model.Author,
                Price = model.Price,
                SalePrice = model.SalePrice,
                OldPrice = model.OldPrice,
                Hours = model.Hours,
                Likes = model.Likes,
            };

            var result = await _courseRepository.CreateAsync(Entity);

            if (result != null)
            {

                return Created();
            }
            else
            {
                return Conflict("Could not create course");
            }
        }

        return BadRequest();
    }

    [HttpGet]

    public async Task<IActionResult> GetAll(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {

        var courses = await _courseService.GetAllAsync(category, searchQuery);

        CourseResult result = new CourseResult
        {
            Succeeded = true,
            TotalItems = courses.Count()
        };

        result.TotaltPages = (int)Math.Ceiling(result.TotalItems / (double)pageSize);
        result.Course = courses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        if (courses != null)
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetOne(int id)
    {
        var course = await _courseRepository.GetOneAsync(x => x.Id == id);

        if (course != null)
        {

            CourseModel model = new CourseModel
            {
                Id = course.Id,
                Image = course.Image,
                BestSeller = course.BestSeller,
                Title = course.Title,
                Author = course.Author,
                Price = course.Price,
                SalePrice = course.SalePrice,
                OldPrice = course.OldPrice,
                Hours = course.Hours,
                Likes = course.Likes,
                Saved = course.Saved
            };

            return Ok(model);
        }


        return NotFound();
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateOne(CourseModel model)
    {

        if (ModelState.IsValid)
        {

            var exists = await _courseRepository.ExistsAsync(x => x.Id == model.Id);

            if (exists)
            {
                CourseEntity Entity = new CourseEntity
                {
                    Image = model.Image,
                    BestSeller = model.BestSeller,
                    Title = model.Title,
                    Author = model.Author,
                    Price = model.Price,
                    SalePrice = model.SalePrice,
                    OldPrice = model.OldPrice,
                    Hours = model.Hours,
                    Likes = model.Likes,
                };

                var result = await _courseRepository.UpdateAsync(x => x.Id == model.Id, Entity);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return Conflict("Could not update course");
                }
            }

            return NotFound();

        }

        return BadRequest();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteOne(CourseModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _courseRepository.DeleteAsync(x => x.Id == model.Id);

            if (result)
            {
                return Ok();
            }
            return Conflict("Could not delete course.");
        }
        return BadRequest();

    }


}
