using projectName.application.input.services.supplier.dto;
using projectName.application.input.services.supplier.interfaces;
using projectName.application.input.seedWork.repository;
using projectName.domain.aggregates.supplier;
using projectName.domain.aggregates.supplier.commands;
using projectName.domain.aggregates.supplier.handlers;
using projectName.domain.shared.seedWork.notification;

namespace projectName.application.input.services.supplier
{
    public sealed class CreateSupplierService : ICreateSupplierService
    {
        private readonly IDbContext dbContext;
        private readonly ISupplierAppRepository supplierAppRepository;
        private readonly ISupplierRepository supplierRepository;

        public CreateSupplierService(IDbContext dbContext, ISupplierAppRepository supplierAppRepository, ISupplierRepository supplierRepository)
        {
            this.dbContext = dbContext;
            this.supplierAppRepository = supplierAppRepository;
            this.supplierRepository = supplierRepository;
        }

        async Task<ReturnSupplierCreation> ICreateSupplierService.Execute(CreateSupplierCommand data, CancellationToken cancellationToken)
        {
            var notification = Notification.New();
            var handlerCreateSupplier = HandlerCreateSupplier.New(data, supplierRepository, notification);
            var supplier = handlerCreateSupplier.Execute();

            notification.ThrowExceptionIfError();

            await supplierAppRepository.Add(supplier!, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new ReturnSupplierCreation(supplier!.Id);
        }
    }
}
