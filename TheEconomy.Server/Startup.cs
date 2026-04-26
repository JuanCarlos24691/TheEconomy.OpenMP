using Microsoft.Extensions.DependencyInjection;
using TheEconomy.Database;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services;

namespace TheEconomy.Server
{
    internal class Startup : IStartup
    {
        public void Configure(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();
            services.AddSingleton<ServerInformation>();
            services.AddSingleton<Colors>();
            services.AddSingleton<CorrectTextStrings>();
            services.AddSingleton<VerifyUserName>();
            services.AddSingleton<VerifyEmail>();
            services.AddSingleton<VerifyDate>();
            services.AddSingleton<DeleteConversation>();
            services.AddSystemsInAssembly();
        }

        public void Configure(IEcsBuilder builder)
        {
            builder.EnableSampEvents();
            builder.EnablePlayerCommands();
            builder.EnableRconCommands();
        }
    }
}