using Application.EasyDelivery.Core.AutoMapper;
using Application.EasyDelivery.BoundedContextExemplos1.Services;
using AutoMapper;
using Domain.EasyDelivery.BoundedContextExemplos1.Commands;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Infra.Data.Persistent.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infra.Data.Persistent.Repositories.BoundedContextExemplos1;
using Domain.EasyDelivery.BoundedContextExemplos1.Repositories;
using Infra.Data.ReadOnly.Repositories.BoundedContextExemplos1;

namespace UI.Web.IoC
{
    public static class ServicesBootstrapper
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDatabases(services, configuration);

            RegisterAutomapper(services);
            RegisterAppServices(services);
            RegisterCommands(services);
            RegisterRepositories(services);
            
            return services;
        }

        static IServiceCollection RegisterAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            return services;
        }

        static IServiceCollection RegisterAppServices(IServiceCollection services)
        {
            services.AddScoped<IExemploAppService, ExemploAppService>();

            return services;
        }
        static IServiceCollection RegisterCommands(IServiceCollection services)
        {

            services.AddScoped<IRequestHandler<AddExemploCommand, ValidationResult>, ExemploCommandHandler>();

            return services;
        }

        static IServiceCollection RegisterRepositories(IServiceCollection services)
        {
            //Persistent
            services.AddScoped<IExemploPersistentRepository, ExemploPersistentRepository>();

            //ReadOnly
            services.AddScoped<IExemploReadOnlyRepository, ExemploReadOnlyRepository>();

            
            return services;
        }
        static IServiceCollection RegisterDatabases(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<EasyDbContext>();
            services.AddDbContext<EasyDbContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}