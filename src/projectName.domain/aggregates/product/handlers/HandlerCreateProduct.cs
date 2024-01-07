using projectName.domain.aggregates.product;
using projectName.domain.aggregates.product.commands;
using projectName.domain.aggregates.product.validations;
using projectName.domain.seedWork.aggregateHandler;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.product.handlers
{
    public sealed class HandlerCreateProduct : HandlerCreateAggregate<Product>
    {
        private readonly CreateProductCommand data;

        private HandlerCreateProduct(CreateProductCommand data, INotification notification) : base(notification)
        {
            this.data = data;
        }
        public static HandlerCreateProduct New(CreateProductCommand data, INotification notification)
            => new HandlerCreateProduct(data, notification);

        protected override Product? execute()
        {
            return Product.Create(data, notification);
        }
    }
}
