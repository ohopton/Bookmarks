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

            return await this.database.QueryAsync<Bookmark>("dbo.Bookmark_GetByUser", new { UserId = userId });
        }

        public async Task<Bookmark> CreateBookmark(Bookmark bookmark)
        {
            this.logger.LogInformation($"Creating new bookmark {bookmark}");

            return await this.database.QuerySingleAsync<Bookmark>("dbo.Bookmark_Create",
                new
                {
                    bookmark.Name,
                    bookmark.UserId,
                    bookmark.Uri
                });
        }

        public async Task<Bookmark> GetBookmark(int id, string userId)
        {
            this.logger.LogInformation($"Getting bookmark {id} for user {userId}");

            return await this.database.QuerySingleAsync<Bookmark>("dbo.Bookmark_GetByIdAndUser",
                new
                {
                    Id = id,
                    UserId = userId
                });
        }
    }
}
