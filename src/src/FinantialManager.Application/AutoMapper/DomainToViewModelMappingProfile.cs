using AutoMapper;
using FinantialManager.Application.ViewModels;
using FinantialManager.Domain.Models;

namespace FinantialManager.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<OFX, OFXViewModel>();
        }
    }
}
