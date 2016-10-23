using System.Collections.Generic;
using System.Threading.Tasks;
using Bookmarks.Logic.Model;

namespace Bookmarks.Logic
{
    public interface IBookmarkManager
    {
        Task<IReadOnlyList<Bookmark>> GetBookmarks(string userId);

        Task<Bookmark> CreateBookmark(Bookmark bookmark);

        Task<Bookmark> GetBookmark(int id, string userId);
    }
}
