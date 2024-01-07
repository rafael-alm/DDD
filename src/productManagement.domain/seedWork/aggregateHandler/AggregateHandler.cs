using productManagement.domain.seedWork.entities.interfaces;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.seedWork.aggregateHandler
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
