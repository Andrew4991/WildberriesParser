using AutoMapper;
using Parser.BL.Data.Models;
using Parser.BL.Data.Models.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BL.Data.Mapping.Api
{
    public class ApiProfile: Profile
    {
        public ApiProfile()
        {
            CreateMap<ApiProductInfo, ProductInfo>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Feedbacks, opt => opt.MapFrom(src => src.Feedbacks))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PriceU % 100 == 0 ? src.PriceU / 100.0 : src.PriceU/100));
        }
    }
}
