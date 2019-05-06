using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using sirena.Models.DBModels;
using sirena.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sirena.Helpers;

namespace sirena.Infrustructure
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            #region Product
            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled.HasValue))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(s => s.ProductCategory.Select(t => t.Category)))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.ProductColor.Select(t => t.Color)))
                .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSize.Select(t => t.Size)));

            CreateMap<ProductVM, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.HasValue ? src.CreatedOn.Value : DateTime.Now))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled ? DateTime.Now : new DateTime?()));
                //.ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.));

            CreateMap<Product, ProductCVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled.HasValue));
            //.ForMember(dest => dest.Category, opt => opt.MapFrom(s => s.Category));

            CreateMap<ProductCVM, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.HasValue ? src.CreatedOn.Value : DateTime.Now))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled ? DateTime.Now : new DateTime?()));

            CreateMap<Product, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Disabled, opt => opt.MapFrom(src => src.IsDisabled.HasValue));

            #endregion

            #region Category
            CreateMap<Category, CategoryVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled.HasValue));

            CreateMap<Category, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Disabled, opt => opt.MapFrom(src => src.IsDisabled.HasValue));

            CreateMap<CategoryVM, Category>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.HasValue ? src.CreatedOn.Value : DateTime.Now))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled ? DateTime.Now : new DateTime?()));

            #endregion

            #region Contact

            #endregion

            #region Color

            CreateMap<Color, ColorVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled.HasValue))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductColor.Select(t => t.Product)));

            CreateMap<ColorVM, Color>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.HasValue ? src.CreatedOn.Value : DateTime.Now))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled ? DateTime.Now : new DateTime?()));

            CreateMap<Color, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Disabled, opt => opt.MapFrom(src => src.IsDisabled.HasValue));

            CreateMap<ColorCVM, Color>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.HasValue ? src.CreatedOn.Value : DateTime.Now))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled ? DateTime.Now : new DateTime?()));

            #endregion

            #region Size

            CreateMap<Size, SizeVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled.HasValue))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductSize.Select(t => t.Product)));

            CreateMap<SizeVM, Size>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.HasValue ? src.CreatedOn.Value : DateTime.Now))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled ? DateTime.Now : new DateTime?()));

            CreateMap<SizeCVM, Size>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : Guid.NewGuid()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.HasValue ? src.CreatedOn.Value : DateTime.Now))
                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => src.IsDisabled ? DateTime.Now : new DateTime?()));

            CreateMap<Size, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Disabled, opt => opt.MapFrom(src => src.IsDisabled.HasValue));
            #endregion
        }
    }    
}
