using productManagement.domain.aggregates.product;
using productManagement.domain.aggregates.product.commands;
using productManagement.domain.aggregates.product.validations;
using productManagement.domain.aggregates.supplier.commands;
using productManagement.domain.seedWork.aggregateHandler;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.aggregates.product.handlers
{
    public sealed class HandlerModifyProduct : AggregateHandler<Product>
    {
        private readonly ModifyProductCommand data;

        private HandlerModifyProduct(ModifyProductCommand data, INotification notification) : base(notification)
        {
            this.data = data;
        }

        public static HandlerModifyProduct New(ModifyProductCommand data, INotification notification)
            => new HandlerModifyProduct(data, notification);

        protected override void execute(Product entity)
        {
            entity.Modify(data, notification);
        }
    }
}
