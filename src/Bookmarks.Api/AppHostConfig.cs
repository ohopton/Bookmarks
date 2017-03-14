using System;

namespace Bookmarks.Api
{
    public class AppHostConfig : IAppHostConfig
    {
        public Uri BaseUri { get; set; }
        public string Auth0ClientId { get; set; }
        public string Auth0ClientSecret { get; set; }
        public string Auth0Domain { get; set; }
    }
}
