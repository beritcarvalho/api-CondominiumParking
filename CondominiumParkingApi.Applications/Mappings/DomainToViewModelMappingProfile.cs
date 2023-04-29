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

            CreateMap<ParkingSpace, ParkingSpaceViewModel>()
                .ForMember(dest => dest.Free, opt => opt.MapFrom(src => true));

            CreateMap<Parked, ParkingSpaceViewModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Free, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Plate, opt => opt.MapFrom(src => src.ApartmentVehicle.Vehicle.Plate))
                .ForMember(dest => dest.Apartment, opt => opt.MapFrom(src => string.Format($"{src.ApartmentVehicle.Apartment.Number}-{src.ApartmentVehicle.Apartment.Block.Block_Name}")))
                .ForMember(dest => dest.In_Date, opt => opt.MapFrom(src => src.In_Date))
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline));

            #endregion

            #region ApartmentVehicle


            #endregion

        }

    }
}
