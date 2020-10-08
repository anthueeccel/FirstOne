using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Infra.Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FirstOne.Cadastros.Api
{
    public class ServicesRegistrator
    {
        public static void RegisterService(IServiceCollection services)
        {
            //AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));

            //Mediator
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Application
            services.AddScoped<IPessoaAppService, PessoaAppService>();

            //Domain - Commands
            services.AddScoped<IRequestHandler<AddPessoaCommand, bool>, PessoaCommandHandler>();

            //Infra - Data
            services.AddScoped<IPessoaRepository, PessoaRepository>();
        }
    }
}
