using projectName.domain.aggregates.product.commands;
using projectName.domain.aggregates.product.validations;
using projectName.domain.seedWork.aggregateHandler;
using projectName.domain.seedWork.entities;
using projectName.domain.seedWork.entities.interfaces;
using projectName.domain.shared.enumeration;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.product
{
    public sealed class Product : EntityWithGuid, IAggregateRoot<Product>
    {
        private Product(string description, StatusEntityEnum status, DateOnly? manufacturingDate, DateOnly? expirationDate, Guid? supplierId)
        {
            Id = Guid.NewGuid();
            Description = description;
            Status = status;
            ManufacturingDate = manufacturingDate;
            ExpirationDate = expirationDate;
            SupplierId = supplierId;
        }

        public Guid? SupplierId { get; private set; }
        public int Code { get; private set; }
        public string Description { get; private set; }
        public StatusEntityEnum Status { get; private set; }
        public DateOnly? ManufacturingDate { get; private set; }
        public DateOnly? ExpirationDate { get; private set; }

        public void Handler(params IAggregateHandler<Product>[] handler)
        {
            foreach (var manipulador in handler)
            {
                manipulador.Execute(this);
                if (!manipulador.Success)
                    break;
            }
        }

        internal static Product? Create(CreateProductCommand data, INotification notification)
        {
            ValidateProductCreation.Execute(data, notification);
            return notification.HasError ? null : new Product(data.Description, StatusEntityEnum.Active, data.ManufacturingDate, data.ExpirationDate, data.SupplierId);
        }

        internal Product Modify(ModifyProductCommand data, INotification notification)
        {
            ValidateProductModification.Execute(data, notification);

            if (!notification.HasError)
            {
                Description = data.Description;
                ManufacturingDate = data.ManufacturingDate;
                ExpirationDate = data.ExpirationDate;
                SupplierId = data.SupplierId;
            }

            return this;
        }

        internal Product Inactivate(INotification notification)
        {
            ValidateProductInactivation.Execute(Status, notification);

            if (!notification.HasError)
                Status = StatusEntityEnum.Inactive;

            return this;
        }
    }
}
