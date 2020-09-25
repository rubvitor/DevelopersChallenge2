using FinantialManager.Application.Interfaces;
using FinantialManager.Application.Services;
using FinantialManager.Domain.Commands;
using FinantialManager.Domain.Core.Events;
using FinantialManager.Domain.Events;
using FinantialManager.Domain.Interfaces;
using FinantialManager.Infra.CrossCutting.Bus;
using FinantialManager.Infra.Data.Context;
using FinantialManager.Infra.Data.EventSourcing;
using FinantialManager.Infra.Data.Repository;
using FinantialManager.Infra.Data.Repository.EventSourcing;
using FinantialManager.Services.Business;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;

namespace FinantialManager.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IOFXAppService, OFXAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<OFXRegisteredEvent>, OFXEventHandler>();
            services.AddScoped<INotificationHandler<OFXUpdatedEvent>, OFXEventHandler>();
            services.AddScoped<INotificationHandler<OFXRemovedEvent>, OFXEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewOFXCommand, ValidationResult>, OFXCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOFXCommand, ValidationResult>, OFXCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOFXCommand, ValidationResult>, OFXCommandHandler>();

            // Infra - Data
            services.AddScoped<IOFXRepository, OFXRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<FinantialManagerContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();

            services.AddScoped<IOFXBusiness, OFXBusiness>();
        }
    }
}