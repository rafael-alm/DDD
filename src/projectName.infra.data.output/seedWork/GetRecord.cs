using Dapper;
using projectName.domain.shared.seedWork.exceptions;

namespace projectName.infra.data.output.seedWork
{
    public class GetRecord<TResult>
    {
        public async static Task<TResult> SingleOrDefaultAsync(SqlFactory factory, string query, object filter, Func<dynamic, TResult> mapping, CancellationToken cancellationToken = default)
        {
            using var connection = factory.SqlConnection();
            var result = await connection.QueryAsync(query, filter).WaitAsync(cancellationToken);

            if (result.Count() == 0)
                throw new NotFoundException("Registro não encontrada");

            var record = result.Select(mapping).SingleOrDefault();
            
            return record!;
        }
    }
}
