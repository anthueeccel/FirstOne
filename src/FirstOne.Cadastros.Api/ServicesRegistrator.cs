using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FirstOne.Cadastros.Api
{
    public class ServicesRegistrator
    {
        public static void RegisterService(IServiceCollection services)
        {
            //AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));

            //Application
            services.AddScoped<IPessoaAppService, PessoaAppService>();

            //Infra - Data
            services.AddScoped<IPessoaRepository, PessoaRepository>();
        }
    }
}
