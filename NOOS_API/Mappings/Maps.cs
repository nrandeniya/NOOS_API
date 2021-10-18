using AutoMapper;
using NOOS_API.Data;
using NOOS_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Subscription, SubscriptionDTO>().ReverseMap();
            CreateMap<Subscription, ButtonTriggerDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Buyer, BuyerDTO>().ReverseMap();
        }
    
    }
}
