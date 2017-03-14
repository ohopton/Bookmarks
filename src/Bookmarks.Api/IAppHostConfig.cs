using System;

namespace Bookmarks.Api
{
    public interface IAppHostConfig
    {
        Uri BaseUri { get; }
        string Auth0ClientId { get; }
        string Auth0ClientSecret { get; }
        string Auth0Domain { get; }
    }
}
