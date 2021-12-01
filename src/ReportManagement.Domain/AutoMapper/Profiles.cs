using AutoMapper;
using ReportManagement.Application.Common;
using ReportManagement.Application.Dtos;
using ReportManagement.Domain.Models;

namespace ReportManagement.Application.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<ReportDto, ReportModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<ReportModel, ICreateReportCommand>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<ICreateReportCommand, ReportModel>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<ReportModel, ReportDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
