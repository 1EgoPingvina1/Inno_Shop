using AutoMapper;
using Inno_Shop.Product.Application.DTO;

namespace Inno_Shop.Product.Application.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Domain.Model.Product, ProductDTO>();
    }
}