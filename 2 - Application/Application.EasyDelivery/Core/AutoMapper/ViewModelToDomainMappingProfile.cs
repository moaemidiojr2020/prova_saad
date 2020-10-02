using Application.EasyDelivery.BoundedContextExemplos1.ViewModels;
using AutoMapper;
using Domain.EasyDelivery.BoundedContextExemplos1.Commands;

namespace Application.EasyDelivery.Core.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
         public ViewModelToDomainMappingProfile()
        {
            CreateMap<AddExemploVM, AddExemploCommand>()
                .ConstructUsing(c => new AddExemploCommand(c.nome, c.email, c.dataNascimento));
        }
    }
}