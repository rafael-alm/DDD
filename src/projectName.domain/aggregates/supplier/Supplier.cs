using projectName.domain.aggregates.supplier.commands;
using projectName.domain.aggregates.supplier.validations;
using projectName.domain.objectValues;
using projectName.domain.seedWork.aggregateHandler;
using projectName.domain.seedWork.entities;
using projectName.domain.seedWork.entities.interfaces;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.product
{
    public sealed class Supplier : EntityWithGuid, IAggregateRoot<Supplier>
    {
        private Supplier(string description, CNPJ cnpj)
        {
            Id = Guid.NewGuid();
            Description = description;
            CNPJ = cnpj;
        }

        public int Code { get; init; }
        public string Description { get; private set; }
        public CNPJ CNPJ { get; private set; }

        public void Handler(params IAggregateHandler<Supplier>[] handler)
        {
            foreach (var manipulador in handler)
            {
                manipulador.Execute(this);
                if (!manipulador.Success)
                    break;
            }
        }

        internal static Supplier? Create(CreateSupplierCommand data, INotification notification)
        {
            ValidateSupplierCreation.Execute(data, notification);

            return notification.HasError ? null : new Supplier(data.Description, data.CNPJ);
        }

        internal Supplier Modify(ModifySupplierCommand data, INotification notification)
        {
            ValidateSupplierModification.Execute(data, notification);

            if (!notification.HasError)
            {
                Description = data.Description;
                CNPJ = data.CNPJ;
            }

            return this;
        }
    }
}
