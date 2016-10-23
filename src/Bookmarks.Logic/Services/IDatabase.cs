using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookmarks.Logic.Services
{
    public interface IDatabase
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param);
        Task<T> QuerySingleAsync<T>(string sql, object param);
    }
}
