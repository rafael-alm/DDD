namespace projectName.infra.data.input.seedWork
{
    public interface IRepository<TModel, TId>
        where TModel : class
        where TId : struct
    {
    }
}
