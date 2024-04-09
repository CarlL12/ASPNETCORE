using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class ContactController(ContactRepository repo) : ControllerBase
{
    private readonly ContactRepository repo = repo;

    [HttpPost]

    public async Task<IActionResult> Create(ContactFormModel model)
    {
        if (ModelState.IsValid)
        {
            var entity = new ContactEntity
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                Service = model.Service,
            };

            var result = await repo.CreateAsync(entity);

            if(result != null)
            {
                return Created("", null);
            }
            else
            {
                return Conflict("Unable to send message.");
            }
        }

        return BadRequest();
    }
}
