using AutoMapper;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Applications.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        private double? teste;
        public DomainToViewModelMappingProfile()
        {
            #region Parked

            CreateMap<Parked, ParkedViewModel>()
                .ForMember(dest => dest.Total_Exceeded_Minutes, opt => opt.MapFrom(src => src.Total_Exceeded_Minutes));
                
            
            #endregion

            #region ParkingSpace

            CreateMap<ParkingSpace, ParkingSpaceViewModel>();

            #endregion

            #region ApartmentVehicle


            #endregion

        }

    }
}
