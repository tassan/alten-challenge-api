using AutoMapper;
using CancunHotel.Application.ViewModels;
using CancunHotel.Domain.Entities;

namespace CancunHotel.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Reservation, CreateBookingViewModel>();
            CreateMap<Reservation, ReadBookingViewModel>();
        }
    }
}
