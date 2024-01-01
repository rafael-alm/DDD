using productManagement.domain.aggregates.product;
using productManagement.domain.aggregates.product.commands;
using productManagement.domain.aggregates.product.validations;
using productManagement.domain.seedWork.aggregateHandler;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.aggregates.product.handlers
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
