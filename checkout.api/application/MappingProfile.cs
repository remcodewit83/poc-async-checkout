using Application.Domain;
using Application.Domain.Events;
using AutoMapper;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cart, CartUpdated>();
        }
    }
}
