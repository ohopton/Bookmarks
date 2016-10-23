using System.Net;
using Bookmarks.ServiceModel.Dto;
using ServiceStack;

namespace Bookmarks.ServiceModel
{
    [Route("/bookmark/{Id}", "GET")]
    [ApiResponse(HttpStatusCode.OK, "Bookmark was returned")]
    [ApiResponse(HttpStatusCode.NotFound, "Bookmark not found")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Something went wrong")]
    public class GetBookmark : IGet, IReturn<BookmarkDto>
    {
        [ApiMember(IsRequired = true, DataType = "int", Description = "The bookmark Id", ParameterType = "query")]
        public int Id { get; set; }
    }
}
