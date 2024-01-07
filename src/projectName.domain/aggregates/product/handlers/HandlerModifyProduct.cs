using projectName.domain.aggregates.product;
using projectName.domain.aggregates.product.commands;
using projectName.domain.aggregates.product.validations;
using projectName.domain.aggregates.supplier.commands;
using projectName.domain.seedWork.aggregateHandler;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.product.handlers
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
