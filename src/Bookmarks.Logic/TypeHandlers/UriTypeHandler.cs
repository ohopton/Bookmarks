using System;
using System.Data;
using static Dapper.SqlMapper;

namespace Bookmarks.Logic.TypeHandlers
{
    public class UriTypeHandler : TypeHandler<Uri>
    {
        public override void SetValue(IDbDataParameter parameter, Uri value)
        {
            parameter.Value = value.ToString();
        }

        public override Uri Parse(object value)
        {
            return new Uri((string) value);
        }
    }
}
