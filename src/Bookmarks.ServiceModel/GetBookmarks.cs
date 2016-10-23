using System.Collections.Generic;
using Bookmarks.ServiceModel.Dto;
using ServiceStack;

namespace Bookmarks.ServiceModel
{
    [Route("/bookmarks", "GET")]
    public class GetBookmarks : IGet, IReturn<IList<BookmarkDto>>
    {
    }
}
