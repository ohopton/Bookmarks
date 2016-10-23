using System.Collections.Generic;
using System.Threading.Tasks;
using Bookmarks.Logic.Model;
using Bookmarks.Logic.Services;
using Microsoft.Extensions.Logging;

namespace Bookmarks.Logic
{
    public class BookmarkManager : IBookmarkManager
    {
        private readonly ILogger<BookmarkManager> logger;
        private readonly IDatabase database;

        public BookmarkManager(ILogger<BookmarkManager> logger, IDatabase database)
        {
            this.logger = logger;
            this.database = database;
        }

        public async Task<IReadOnlyList<Bookmark>> GetBookmarks(string userId)
        {
            this.logger.LogInformation($"Getting bookmarks for user {userId}");
            return await this.database.QuerySync<Bookmark>("dbo.Bookmark_GetByUser", new { UserId = userId });
        }
    }
}
