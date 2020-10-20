using AutoMapper;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Entities;

namespace FirstOne.Cadastros.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Pessoa, PessoaViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}
