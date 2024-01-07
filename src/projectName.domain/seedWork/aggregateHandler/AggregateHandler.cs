using projectName.domain.seedWork.entities.interfaces;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.seedWork.aggregateHandler
{
    public abstract class AggregateHandler<TEntity> : IAggregateHandler<TEntity>
     where TEntity : IAggregateRoot<TEntity>, IEntity
    {
        protected readonly INotification notification;

        protected AggregateHandler(INotification notificacao)
            => this.notification = notificacao;

        bool IAggregateHandler.Success => !notification.HasError;
        public INotification Notification => notification;

        void IAggregateHandler<TEntity>.Execute(TEntity entity)
            => execute(entity);

        protected abstract void execute(TEntity entity);
    }

}
