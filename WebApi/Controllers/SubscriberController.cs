using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;


namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class SubscriberController(SubscriberService subscriberService) : ControllerBase
{
    private readonly SubscriberService _subscriberService = subscriberService;

    [HttpPost]     
    public async Task<IActionResult> Create(string email)
    {
        if (ModelState.IsValid)
        {
            var exists = await _subscriberService.GetOne(email);

            if(exists == null)
            {
                var result = await _subscriberService.Create(email);

                if (result)
                {
                    return Created("", null);
                }
                else
                {
                    return Conflict("Unable to create subscription.");
                }
            }
            else
            {
                return Conflict("Your email address is already subscribed.");
            }
        }
        return BadRequest();
    }

    [HttpDelete]

    public async Task<IActionResult> Delete (string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            var exists = await _subscriberService.GetOne(email);

            if (exists != null)
            {
                var result = await _subscriberService.Delete(email);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return Conflict("Unable to delete subscription.");
                }
            }
            else
            {
                return Conflict("Your email address is not subscribed.");
            }
        }
        return BadRequest();
    }
}
