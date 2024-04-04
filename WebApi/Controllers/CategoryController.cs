using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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
