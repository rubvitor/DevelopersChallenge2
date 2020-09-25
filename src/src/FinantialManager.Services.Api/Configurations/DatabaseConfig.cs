using System;
using System.Threading.Tasks;
using FinantialManager.Domain.Interfaces;
using FinantialManager.Domain.Models;
using FinantialManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinantialManager.Services.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<FinantialManagerContext>(options =>
                options.UseInMemoryDatabase(databaseName: configuration.GetValue<string>("DatabaseName")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddDbContext<EventStoreSqlContext>(options =>
                options.UseInMemoryDatabase(databaseName: configuration.GetValue<string>("DatabaseName")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
    }

    public class ELPIdentityInitializer
    {
        private readonly IUserRepository _userRepository;

        public ELPIdentityInitializer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Seed()
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "xayah",
                PasswordHash = "123456",
                Name = "Xayah",
                Email = "xayah@esports.com"
            };

            _userRepository.Add(user);

            await _userRepository.UnitOfWork.Commit();
        }
    }
}