using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace TrainingTake2.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //todo: move to web.config or startupAuth
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});

            ////todo: use DI
            //using (var _repo = new AuthRepository())
            //{
            //    var user = await _repo.FindUser(context.UserName, context.Password);

            //    if (user == null)
            //    {
            //        context.SetError("invalid_grant", "The user name or password is incorrect.");
            //        return;
            //    }
            //}

            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", "user"));

            //context.Validated(identity);
        }
    }
}