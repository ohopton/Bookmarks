using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookmarks.Logic.Services
{
    public interface IDatabase
    {
        Task<IReadOnlyList<T>> QuerySync<T>(string sql, object param);
    }
}
