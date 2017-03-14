using System;
using Bookmarks.ServiceInterface;
using Funq;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Auth;
using ServiceStack.Validation;
using Text = ServiceStack.Text;

namespace Bookmarks.Api
{
    public class AppHost : AppHostBase
    {
        private readonly IAppHostConfig configuration;

        public AppHost(IAppHostConfig configuration) : base("Bookmarks.Api", typeof(BookmarkService).GetAssembly())
        {
            this.configuration = configuration;
        }

        public override void Configure(Container container)
        {
            // Format dates as ISO8601
            Text.JsConfig<DateTime>.SerializeFn = d => d.ToString("yyyy-MM-ddTHH:mm:ssZ");
            Text.JsConfig<DateTime?>.SerializeFn = d => d?.ToString("yyyy-MM-ddTHH:mm:ssZ");

            // get the Uri we're running on
            Uri baseUri = this.configuration.BaseUri;

            // if we're on a https url then we require SSL
            bool requireSsl = baseUri.Scheme == "https";

            // set up config
            HostConfig config = new HostConfig
            {
                WebHostUrl = baseUri.ToString(),
                UseHttpsLinks = requireSsl,
                DefaultRedirectPath = "swagger-ui/"
            };

            this.SetConfig(config);

            // enable swagger
            this.Plugins.Add(new SwaggerFeature());

            // enable validation
            this.Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(BookmarkService).GetAssembly());

            // set up JWT authentication
            this.Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] {
                    new JwtAuthProviderReader {
                        HashAlgorithm = "HS256",
                        Audience = this.configuration.Auth0ClientId,
                        AuthKeyBase64 = this.configuration.Auth0ClientSecret,
                        Issuer = this.configuration.Auth0Domain,
                        RequireSecureConnection = requireSsl,
                        PopulateSessionFilter = (session, payload, request) =>
                        {
                            // for some reason by default this reader loses the auth0| prefix from the user's id when it populates
                            // the userauthid, so we'll override that behaviour here
                            session.UserAuthId = payload.Child("sub");
                        }
                    }}));
        }
    }
}
