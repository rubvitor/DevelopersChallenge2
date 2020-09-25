using AutoMapper;
using FinantialManager.Application.ViewModels;
using FinantialManager.Domain.Commands;

namespace FinantialManager.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<OFXViewModel, RegisterNewOFXCommand>()
                .ConstructUsing(c => new RegisterNewOFXCommand(c.OFX));
            CreateMap<OFXViewModel, UpdateOFXCommand>()
                .ConstructUsing(c => new UpdateOFXCommand(c.OFX));
        }
    }
}
