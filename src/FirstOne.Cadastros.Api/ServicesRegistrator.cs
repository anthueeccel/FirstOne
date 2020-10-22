using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Application.Token;
using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Commands.Usuario;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using FirstOne.Cadastros.Infra.Data.Context;
using FirstOne.Cadastros.Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FirstOne.Cadastros.Api
{
    public class ServicesRegistrator
    {
        public static void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMvc();

            //Token settings
            var tokenSettingsSection = configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(tokenSettingsSection);
            var tokenSettings = tokenSettingsSection.Get<TokenSettings>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.Secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = tokenSettings.Issuer,
                    ValidAudience = tokenSettings.Audience
                };
            });

            //AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));

            //Mediator
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Application
            services.AddScoped<IPessoaAppService, PessoaAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();

            //Domain - Commands
            services.AddScoped<IRequestHandler<AddPessoaCommand, Unit>, PessoaCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePessoaCommand, Unit>, PessoaCommandHandler>();
            services.AddScoped<IRequestHandler<RemovePessoaCommand, Unit>, PessoaCommandHandler>();
            services.AddScoped<IRequestHandler<AddUsuarioCommand, Unit>, UsuarioCommandHandler>();

            //Infra - Data
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<SqlServerContext>();

            var x = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SqlServerContext>(options =>
                options.UseSqlServer(x));
        }
    }
}
