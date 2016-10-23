using System;

namespace Bookmarks.Logic.Model
{
    public class Bookmark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public Uri Uri { get; set; }
    }
}
