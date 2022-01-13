using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace WalletService.Common
{
    public class RepositoryBase
    {
        protected readonly string ConnectionString;

        protected RepositoryBase(IOptions<DbOptions> options)
        {
            ConnectionString = options.Value.ConnectionString;
        }

        protected Task<IEnumerable<TEntity>> SelectAsync<TEntity>(CancellationToken token)
        {
            using MySqlConnection connection = new MySqlConnection(ConnectionString);
            return connection.QueryAsync<TEntity>($"SELECT * FROM {GetTableName(typeof(TEntity))}");
        }
        private static string GetTableName(MemberInfo type)
        {
            return type.GetCustomAttribute<TableAttribute>()?.Name ?? type.Name;
        }
    }
}
