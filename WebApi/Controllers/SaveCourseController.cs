using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
[Authorize]
public class SaveCourseController(CourseService service) : ControllerBase
{

    private readonly CourseService service = service;

    [HttpPost]

    public async Task<IActionResult> Create(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id value");
        }

        var result = await service.SaveOrDeleteCourse(id);

        if(result)
        {
            return Created();
        }
        else
        {
            return Ok();
        }

    }

    [HttpDelete] 

    public async Task<IActionResult> Delete()
    {
        var result = await service.DeleteSavedCourses();

        if (result)
        {
            return Ok();
        }

        return BadRequest("Could not delete courses");
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var courses = await service.GetAllSavedCourses();

        if(courses != null)
        {
            return Ok(courses);
        }
        else
        {
            return NotFound();
        }
       
    }
}
