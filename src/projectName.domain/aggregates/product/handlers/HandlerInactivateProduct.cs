using projectName.domain.seedWork.aggregateHandler;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.product.handlers
{
    public sealed class HandlerInactivateProduct : AggregateHandler<Product>
    {
        private HandlerInactivateProduct(INotification notification) : base(notification)
        {
        }

        public static HandlerInactivateProduct New(INotification notification)
            => new HandlerInactivateProduct(notification);

        protected override void execute(Product entity)
            => entity.Inactivate(notification);
    }
}
