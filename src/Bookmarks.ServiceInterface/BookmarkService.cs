using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bookmarks.Logic;
using Bookmarks.Logic.Model;
using Bookmarks.ServiceModel;
using Bookmarks.ServiceModel.Dto;
using ServiceStack;

namespace Bookmarks.ServiceInterface
{
    [Authenticate]
    public class BookmarkService : Service
    {
        private readonly IBookmarkManager bookmarkManager;

        private string UserId => this.GetSession()?.UserAuthId ?? string.Empty;

        public BookmarkService(IBookmarkManager bookmarkManager)
        {
            this.bookmarkManager = bookmarkManager;
        }

        public async Task<object> Get(GetBookmarks request)
        {
            IReadOnlyList<Bookmark> bookmarks = await this.bookmarkManager.GetBookmarks(this.UserId);
            return new HttpResult(bookmarks.Select(b => b.ConvertTo<BookmarkDto>()), HttpStatusCode.OK);
        }

        public async Task<object> Get(GetBookmark request)
        {
            Bookmark bookmark = await this.bookmarkManager.GetBookmark(request.Id, this.UserId);
            return bookmark != null ? new HttpResult(bookmark.ConvertTo<BookmarkDto>(), HttpStatusCode.OK) : new HttpResult(HttpStatusCode.NotFound);
        }

        public async Task<object> Post(CreateBookmark request)
        {
            Bookmark bookmark = new Bookmark()
            {
                Name = request.Name,
                UserId = this.UserId,
                Uri = new Uri(request.Uri)
            };

            Bookmark createdBookmark = await this.bookmarkManager.CreateBookmark(bookmark);
            GetBookmark newLocation = new GetBookmark() { Id = createdBookmark.Id };

            return HttpResult.Status201Created(createdBookmark.ConvertTo<BookmarkDto>(), newLocation.ToAbsoluteUri());
        }
    }
}
