using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bookmarks.Logic.Services
{
    public class Database : IDatabase
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        private readonly ILogger<Database> logger;

        public Database(IDbConnectionFactory dbConnectionFactory, ILogger<Database> logger)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            this.logger = logger;
        }

        private async Task<IReadOnlyList<T>> InternalQueryAsync<T>(string sql, object param)
        {
            using (IDbConnection connection = this.dbConnectionFactory.Open())
            {
                IEnumerable<T> results = await connection.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param)
        {
            this.logger.LogInformation($"Executing {sql} with parameters {param} to get IReadOnlyList<{typeof(T).Name}>");
            return await this.InternalQueryAsync<T>(sql, param);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param)
        {
            this.logger.LogInformation($"Executing {sql} with parameters {param} to get {typeof(T).Name}");

            IReadOnlyList<T> results = await this.InternalQueryAsync<T>(sql, param);
            return results.SingleOrDefault();
        }
    }
}
