using Dapper;
using productManagement.application.output.seedWork;
using System.Data;

namespace productManagement.infra.data.output.seedWork
{
    public class Paging<TResult> : IPaging<TResult>
    {
        private Paging(IEnumerable<TResult> records, int page, int recordsPerPage, int totalRecords)
        {
            Records = records;
            Page = page;
            RecordsPerPage = recordsPerPage;
            TotalRecords = totalRecords;
        }

        public IEnumerable<TResult> Records { get; init; }
        public int Page { get; init; }
        public int RecordsPerPage { get; init; }
        public int TotalRecords { get; init; }

        public async static Task<IPaging<TResult>> ExecuteAsync(SqlFactory factory, string query, string order, IFilterPaging filter, Func<dynamic, TResult> mapping, CancellationToken cancellationToken = default)
        {
            Paging<TResult> paging;

            using var connection = factory.SqlConnection();

            if (filter.Page < 1)
                filter.Page = 1;

            if (filter.RecordsPerPage < 1)
                filter.RecordsPerPage = 1;

            var queryWithPaging = query + order + $"OFFSET (@{nameof(filter.Page)} - 1) * @{nameof(filter.RecordsPerPage)} ROWS FETCH NEXT @{nameof(filter.RecordsPerPage)} ROWS ONLY";
            var result = await connection.QueryAsync(queryWithPaging, filter).WaitAsync(cancellationToken);
            var records = result.Select(mapping);
            var totalRecords = await connection.QuerySingleAsync<int>(adaptToCount(query), filter).WaitAsync(cancellationToken);

            paging = new Paging<TResult>(records, filter.Page, filter.RecordsPerPage, totalRecords);

            return paging;

            static string adaptToCount(string query)
            {
                var startIndex = query.IndexOf("FROM", StringComparison.CurrentCultureIgnoreCase);
                return $"SELECT COUNT(1) {query.Substring(startIndex)}";
            }
        }
    }
}
