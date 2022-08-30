using AutoMapper;
using CancunHotel.Application.ViewModels;
using CancunHotel.Domain.Entities;

namespace CancunHotel.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, Customer>()
                .ConstructUsing(c => new Customer(c.FirstName, c.Email, c.Email, c.BirthDate));

            CreateMap<BookingViewModel, Reservation>()
                .ConstructUsing(b => new Reservation(b.CustomerId, b.CheckInDate, b.CheckOutDate, b.GuestsAmount));
        }
    }
}
