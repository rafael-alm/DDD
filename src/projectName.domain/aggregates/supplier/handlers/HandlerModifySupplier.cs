using projectName.domain.aggregates.product;
using projectName.domain.aggregates.supplier.commands;
using projectName.domain.aggregates.supplier.validations;
using projectName.domain.seedWork.aggregateHandler;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.supplier.handlers
{
    public sealed class HandlerModifySupplier : AggregateHandler<Supplier>
    {
        private readonly ModifySupplierCommand data;
        private readonly ISupplierRepository repositorio;

        private HandlerModifySupplier(ModifySupplierCommand data, ISupplierRepository repositorio, INotification notification) : base(notification)
        {
            this.data = data;
            this.repositorio = repositorio;
        }
        public static HandlerModifySupplier New(ModifySupplierCommand data, ISupplierRepository repositorio, INotification notification)
            => new HandlerModifySupplier(data, repositorio, notification);

        protected override void execute(Supplier entity)
        {
            if (repositorio.CNPJHasAlreadyBeenNotifiedToAnotherSupplier(data.CNPJ, data.Id).Result)
                notification.Add(SupplierMessages.CNPJHasAlreadyBeenInformed);

            if (notification.HasError) return;

            entity.Modify(data, notification);
        }

    }
}
