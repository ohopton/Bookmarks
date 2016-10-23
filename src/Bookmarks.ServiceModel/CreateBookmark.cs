using Bookmarks.ServiceModel.Dto;
using ServiceStack;

namespace Bookmarks.ServiceModel
{
    [Route("/bookmark", "POST")]
    public class CreateBookmark : IPost, IReturn<BookmarkDto>
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}
