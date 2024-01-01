using productManagement.domain.aggregates.product;
using productManagement.domain.aggregates.supplier.commands;
using productManagement.domain.aggregates.supplier.validations;
using productManagement.domain.seedWork.aggregateHandler;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.aggregates.supplier.handlers
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
