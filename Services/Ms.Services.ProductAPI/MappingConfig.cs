﻿using AutoMapper;
using Ms.Services.ProductAPI.Models;
using Ms.Services.ProductAPI.Models.Dto;

namespace Ms.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
           
            });
            return mappingConfig;
        }
    }
}
