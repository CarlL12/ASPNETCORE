
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Infrastructure.Services;

public class SubscriberService(SubscriberRepository subscriberRepository)
{

    private readonly SubscriberRepository _subscriberRepository = subscriberRepository;

    public async Task<bool> Create(string Email)
    {
        try
        {
            SubscriberEntity Entity = new SubscriberEntity
            {
                Email = Email,
            };

            var createResult = await _subscriberRepository.CreateAsync(Entity);

            if(createResult != null)
            {
                return true;
            }
                
            else
            {
                return false;
            }
            
        }

        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }

    public async Task<SubscriberEntity> GetOne(string email)
    {
        try
        {
            var result = await _subscriberRepository.GetOneAsync(x => x.Email == email);

            if(result != null)
            {
                return result;
            }
            else
            {
                return null!;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<bool> Delete (string email)
    {
        try
        {
            var result = await _subscriberRepository.DeleteAsync(x => x.Email == email);

            if(result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false!;
    }
}
