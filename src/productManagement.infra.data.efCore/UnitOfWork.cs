using productManagement.application.input.seedWork.repository;

namespace productManagement.infra.data.input
{
    public class UnitOfWork : IDbContext
    {
        private readonly ContextProductManagement context;

        public UnitOfWork(ContextProductManagement context)
            => this.context = context;

        Task IDbContext.Commit(CancellationToken cancellationToken)
            => context.Commit(cancellationToken);

        ValueTask IDbContext.DisposeAsync()
            => context.DisposeAsync();

        Task IDbContext.Rollback(CancellationToken cancellationToken)
            => context.Rollback();

        Task<int> IDbContext.SaveChangesAsync(CancellationToken cancellationToken)
            => context.SaveChangesAsync();
    }
}
