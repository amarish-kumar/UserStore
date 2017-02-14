using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Owin;
using Training.Identity.Services;
using TrainingTake2.App_Start;
using TrainingTake2.Providers;

namespace TrainingTake2
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
            var repository = UnityConfig.RegisterTypes().Resolve<IAuthRepository>();
            var OAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(repository)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}