using projectName.application.input.seedWork.repository;

namespace projectName.infra.data.input.seedWork
{
    public abstract class Repository<TModel, TId> : IRepository<TModel, TId>
        where TModel : class
        where TId : struct
    {
        protected readonly ContextProductManagement context;
        protected Repository(IDbContext context)
            => this.context = context as ContextProductManagement;
    }
}
