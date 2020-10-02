using AutoMapper;
using Domain.Core.Operacoes;
using Domain.Core.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Infra.Data.Persistent.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infra.Data.Core.ConnectionStrings;
using Infra.Data.Persistent.Repositories.Prova;
using Infra.Data.ReadOnly.Repositories.Prova;
using MediatR;
using Domain.SAAD.Prova.Commands;
using Domain.SAAD.Prova.Repositories;
using Application.SAAD.Core.AutoMapper;
using Application.SAAD.Prova.Interfaces;
using Application.SAAD.Prova.Services;

namespace UI.Web.IoC
{
    public static class ServicesBootstrapper
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            RegisterDatabases(services, configuration);

            RegisterAutomapper(services);
            RegisterAppServices(services);
            RegisterCommands(services);
            RegisterRepositories(services);
            RegisterDataCore(services);
            return services;
        }

        static IServiceCollection RegisterAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
            return services;
        }

        static IServiceCollection RegisterAppServices(IServiceCollection services)
        {
	        services.AddScoped<IPessoaAppService, PessoaAppService>();
            return services;
        }
        static IServiceCollection RegisterCommands(IServiceCollection services)
        {
	        services.AddScoped<IRequestHandler<PessoaCommand, RespostaCommand>, PessoaCommandHandler>();
	        services.AddScoped<IRequestHandler<EditarPessoaCommand, RespostaCommand>, PessoaCommandHandler>();
	        services.AddScoped<IRequestHandler<ExcluirPessoaCommand, RespostaCommand>, PessoaCommandHandler>();
            return services;
        }

        static IServiceCollection RegisterDataCore(IServiceCollection services)
        {
            services.AddSingleton<ConnectionStringHandler>();
            return services;
        }

        static IServiceCollection RegisterRepositories(IServiceCollection services)
        {

	        services.AddScoped<IPessoaPersistentRepository, PessoaPersistentRepository>();
	        services.AddScoped<IPessoaReadOnlyRepository, PessoaReadOnlyRepository>();
            return services;
        }
        static IServiceCollection RegisterDatabases(IServiceCollection services, IConfiguration configuration)
        {

            //UnitOfWOrk
            services.AddScoped<IUoW, UnitOfWork>();
            services.AddDbContext<SaadDbContext>((provider, opts) =>
         {
             var connStringHandler = provider.GetService<ConnectionStringHandler>();
             opts.UseSqlServer(connStringHandler.GetConnectionString());
         });

            return services;
        }
    }
}