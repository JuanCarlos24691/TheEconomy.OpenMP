using Microsoft.Extensions.DependencyInjection;
using TheEconomy.Database;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.KnowledgeTest;
using TheEconomy.Server.Resources.Services.Colors;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.DeleteConversation.Interfaces;
using TheEconomy.Server.Resources.Services.DeleteConversation;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings;
using TheEconomy.Server.Resources.Services.VerifyDate.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyDate;
using TheEconomy.Server.Resources.Services.VerifyMail.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyMail;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyUserName;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation;

namespace TheEconomy.Server
{
    internal class Startup : IStartup
    {
        public void Configure(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();

            services.AddSingleton<IServerInformation, ServerInformation>();
            services.AddSingleton<IVerifyUserName, VerifyUserName>();
            services.AddSingleton<IVerifyMail, VerifyMail>();
            services.AddSingleton<IVerifyDate, VerifyDate>();
            services.AddSingleton<ICorrectTextStrings, CorrectTextStrings>();
            services.AddSingleton<IDeleteConversation, DeleteConversation>();
            services.AddSingleton<IColors, Colors>();

            services.AddTransient<KnowledgeTest>();

            services.AddSystemsInAssembly();
        }

        public void Configure(IEcsBuilder builder)
        {
            builder.EnableSampEvents();
            builder.EnablePlayerCommands();
            // builder.EnableActorEvents();
            // builder.EnablePlayerEvents();
            // builder.EnableObjectEvents();
            // builder.EnableVehicleEvents();
            // builder.EnableRconEvents();
            builder.EnableRconCommands();
        }
    }
}