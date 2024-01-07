using Microsoft.Data.SqlClient;
using System.Data;

namespace projectName.infra.data.output
{
    public sealed class SqlFactory
    {
        public IDbConnection SqlConnection()
            => new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_ProductManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    }
}
