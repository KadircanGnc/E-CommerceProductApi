using AutoMapper;
using Entities;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Brand;
using BusinessLogic.DTOs.Product;
using BusinessLogic.DTOs.User;
using BusinessLogic.DTOs.Category;
using BusinessLogic.DTOs.Comment;

namespace E_CommerceApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Product
            CreateMap<Product, GetProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();

            //Brand
            CreateMap<Brand, GetBrandDTO>().ReverseMap();
            CreateMap<Brand, CreateBrandDTO>().ReverseMap();
            CreateMap<Brand, UpdateBrandDTO>().ReverseMap();
            CreateMap<Brand, GetBrandDTO>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            //Category
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();            
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryDTO>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            //Order
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderProducts!.Select(op => op.Product)));

            //User
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, GetUserDTO>().ReverseMap();

            //Cart
            CreateMap<Cart, CartDTO>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

            //CartItem
            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product!.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            //Comment
            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, GetCommentDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.Name))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name)).ReverseMap();

        }
    }
}
