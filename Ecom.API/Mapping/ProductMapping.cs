using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            
            CreateMap<Photo, PhotoDTO>();

            CreateMap<AddProductDTO, Product>()
                .ForMember(x => x.Photos, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateProductDTO, Product>()
                .ForMember(m => m.Photos, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}
