using AutoMapper;
using Entities;
using BusinessLogic.DTOs;

namespace E_CommerceApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();            
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderProducts!.Select(op => op.Product)));

            CreateMap<Brand, BrandDTO>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<Cart, CartDTO>()
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product!.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
