using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class CategoryController(CategoryService categoryService) : ControllerBase
{
    private readonly CategoryService _categoryService = categoryService;

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        try
        {
           var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }
        catch
        {
            return NotFound();
        }
       
    }
}
