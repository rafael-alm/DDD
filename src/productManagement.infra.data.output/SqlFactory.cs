using Microsoft.Data.SqlClient;
using System.Data;

namespace productManagement.infra.data.output
{
    public sealed class SqlFactory
    {
        public IDbConnection SqlConnection()
            => new SqlConnection("Data Source=localhost;Initial Catalog=DBProductManagement;Integrated Security=False;trusted_connection=true;encrypt=false;User Id=sa; Password=desenvol;");

    }
}
