namespace projectName.application.input.seedWork.repository
{
    public interface IDbContext
    {
        public Task Commit(CancellationToken cancellationToken = default);
        public Task Rollback(CancellationToken cancellationToken = default);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public ValueTask DisposeAsync();
    }
}
