using AutoMapper;
using Inno_Shop.Product.Service.DTO;

namespace Inno_Shop.Product.Service.Helpers.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Model.Product, ProductDTO>();
    }
}