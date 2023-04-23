using AutoMapper;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Applications.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region Parked

            CreateMap<Parked, ParkedViewModel>()
                .ForMember(dest => dest.ParkingSpace, opt => opt.MapFrom(src => src.ParkingSpace))
                .ForMember(dest => dest.ApartmentVehicle, opt => opt.MapFrom(src => src.ApartmentVehicle))
                .ForMember(dest => dest.Exceeded, opt => opt.MapFrom(src => src.Total_Exceeded_Minutes.HasValue))
                .ForMember(dest => dest.Time_Exceeded, opt => opt.MapFrom(src => src.Total_Exceeded_Minutes.HasValue ? TimeSpan.FromMinutes((double)src.Total_Exceeded_Minutes) : TimeSpan.Zero))
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline));
            
            #endregion

            #region ParkingSpace

            CreateMap<ParkingSpace, ParkingSpaceViewModel>();

            #endregion

            #region ApartmentVehicle


            #endregion

        }

    }
}
