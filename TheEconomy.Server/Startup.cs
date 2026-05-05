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
using TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyProhibition;
using TheEconomy.Server.Resources.Authenticator.RegisterAccount.Interfaces;
using TheEconomy.Server.Resources.Authenticator.RegisterAccount.Layouts;
using TheEconomy.Server.Resources.Services.VerifyProhibition.Layouts;
using TheEconomy.Server.Resources.Services.VerifyUserName.Layouts;
using TheEconomy.Server.Resources.BlackBackground.Layouts;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;
using TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Layouts;
using TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Interfaces;
using TheEconomy.Server.Resources.KnowledgeTest.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Login.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Login.Layouts;

namespace TheEconomy.Server
{
    internal class Startup : IStartup
    {
        public void Configure(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();

            services.AddSingleton<IKnowledgeTest, KnowledgeTest>();
            services.AddSingleton<IVerifyUserNameLayout, VerifyUserNameLayout>();
            services.AddSingleton<IVerifyProhibition, VerifyProhibition>();
            services.AddSingleton<IBlackBackgroundLayout, BlackBackgroundLayout>();
            services.AddSingleton<IVerifyProhibitionLayout, VerifyProhibitionLayout>();
            services.AddSingleton<ILoginLayout, LoginLayout>();
            services.AddSingleton<IRegisterCharacterLayout, RegisterCharacterLayout>();
            services.AddSingleton<IRegisterAccountLayout, RegisterAccountLayout>();
            services.AddSingleton<IServerInformation, ServerInformation>();
            services.AddSingleton<IVerifyUserName, VerifyUserName>();
            services.AddSingleton<IVerifyMail, VerifyMail>();
            services.AddSingleton<IVerifyDate, VerifyDate>();
            services.AddSingleton<ICorrectTextStrings, CorrectTextStrings>();
            services.AddSingleton<IDeleteConversation, DeleteConversation>();
            services.AddSingleton<IColors, Colors>();

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