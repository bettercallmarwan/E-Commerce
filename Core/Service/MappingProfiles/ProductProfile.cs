﻿using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, Options => Options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, Options => Options.MapFrom(src => src.ProductType.Name))
                .ForMember(dist => dist.PictureUrl, Options => Options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
        }
    }
}
 