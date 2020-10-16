using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using FirstOne.Cadastros.Infra.Data.Context;
using FirstOne.Cadastros.Infra.Data.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FirstOne.Cadastros.Api
{
    public class ServicesRegistrator
    {
        public static void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            //MongoDb
            MongoDbContext.ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
            MongoDbContext.DatabaseName = configuration.GetSection("MongoConnection:Database").Value;
            MongoDbContext.IsSSL = Convert.ToBoolean(configuration.GetSection("MongoConnection:IsSSL").Value);
            services.AddMvc();

            //AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));

            //Mediator
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Application
            services.AddScoped<IPessoaAppService, PessoaAppService>();

            //Domain - Commands
            services.AddScoped<IRequestHandler<AddPessoaCommand, bool>, PessoaCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePessoaCommand, bool>, PessoaCommandHandler>();
            services.AddScoped<IRequestHandler<RemovePessoaCommand, bool>, PessoaCommandHandler>();

            //Infra - Data
            services.AddScoped<IPessoaRepository, PessoaRepository>();
        }
    }
}
