using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Training.Identity;
using Training.Identity.Services;

namespace Training.API.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAuthRepository _repository;

        public SimpleAuthorizationServerProvider(IAuthRepository repository)
        {
            _repository = repository;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = _repository.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = GetIdentity(context, user);
            var ticket = GetTicket(context, user, identity);
            context.Validated(ticket);
        }

        private AuthenticationTicket GetTicket(OAuthGrantResourceOwnerCredentialsContext context, ApplicationUser user,
            ClaimsIdentity identity)
        {
            var dict = new Dictionary<string, string>
            {
                {
                    "as:client_id", context.ClientId ?? string.Empty
                },
                {
                    "id", user.Id
                }
            };

            foreach (var role in _repository.GetUserRoles(user.Id))
            {
                dict.Add("role", role);
            }

            var prop = new AuthenticationProperties(dict);

            return new AuthenticationTicket(identity, prop);
        }

        private ClaimsIdentity GetIdentity(OAuthGrantResourceOwnerCredentialsContext context, ApplicationUser user)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            foreach (var role in _repository.GetUserRoles(user.Id))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return identity;
        }

        public override async Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value ?? property.Key);
            }
        }
    }
}