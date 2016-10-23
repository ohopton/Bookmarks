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

        public BookmarkService(IBookmarkManager bookmarkManager)
        {
            this.bookmarkManager = bookmarkManager;
        }

        public async Task<object> Get(GetBookmarks request)
        {
            IReadOnlyList<Bookmark> bookmarks = await this.bookmarkManager.GetBookmarks(this.GetSession().UserAuthId);
            return new HttpResult(bookmarks.Select(b => b.ConvertTo<BookmarkDto>()), HttpStatusCode.OK);
        }
    }
}
