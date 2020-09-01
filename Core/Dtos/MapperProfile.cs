using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos
{
    public class MapperProfile: Profile
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapperProfile>();
            });
            return config.CreateMapper();
        }

        public MapperProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.ProductCategory.Name))
                .ForMember(d => d.ProductModel, o => o.MapFrom(s => s.ProductModel.Name));

            CreateMap<Customer, CustomerDto>();
        }
    }
}
