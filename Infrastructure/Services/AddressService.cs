

using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AddressService(AddressRepository addressRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;

    public async Task<AddressInfoModel> GetOrCreateAddress(AddressInfoModel address)
    {
        try
        {
            var result = await _addressRepository.GetOneAsync(x => x.Address1 == address.Address1 && x.PostalCode == address.PostalCode && x.City == address.City);
            if (result == null)
            {
                AddressEntity Entity = new AddressEntity
                {
                    Address1 = address.Address1,
                    Address2 = address.Address2,
                    PostalCode = address.PostalCode,
                    City = address.City,
                };

                var createResult = await _addressRepository.CreateAsync(Entity);
                if (createResult == null)
                {
                    return null!;
                }
                else
                {
                    AddressInfoModel model = new AddressInfoModel
                    {
                        Id = createResult.Id,
                        Address1 = createResult.Address1,
                        Address2 = createResult.Address2,
                        PostalCode = createResult.PostalCode,
                        City = createResult.City,
                    };

                    return model;
                }
            }
            else
            {
                AddressInfoModel model = new AddressInfoModel
                {
                    Id = result.Id,
                    Address1 = result.Address1,
                    Address2 = result.Address2,
                    PostalCode = result.PostalCode,
                    City = result.City,
                };

                return model;
            }
        }

        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<AddressInfoModel> GetAddressAsync(int? id)
    {
        try
        {
            var result = await _addressRepository.GetOneAsync(x => x.Id == id);

            if(result == null)
            {
                return null!;
            }

            else
            {
                var model = new AddressInfoModel
                {
                    Id = result.Id,
                    Address1 = result.Address1,
                    Address2 = result.Address2,
                    PostalCode = result.PostalCode,
                    City = result.City,
                };

                return model;
            }
        }

        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}
