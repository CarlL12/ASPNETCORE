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

    public async Task<IActionResult> GetAll()
    {
        var courses = await _courseService.GetAllAsync();

        if (courses != null)
        {
            return Ok(courses);
        }
        return NotFound();
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetOne(int id)
    {
        var course = await _courseRepository.GetOneAsync(x => x.Id == id);

        if (course != null)
        {
            return Ok(course);
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
