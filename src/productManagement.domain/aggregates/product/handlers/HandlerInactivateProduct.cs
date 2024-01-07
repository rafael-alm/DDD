using productManagement.domain.seedWork.aggregateHandler;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.aggregates.product.handlers
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
