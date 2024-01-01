using productManagement.application.input.services.product.interfaces;
using productManagement.application.input.seedWork.repository;
using productManagement.domain.aggregates.product.commands;
using productManagement.domain.aggregates.product.handlers;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.application.input.services.product
{
    public sealed class ModifyProductService : IModifyProductService
    {
        private readonly IDbContext dbContext;
        private readonly IProductAppRepository productAppRepository;

        public ModifyProductService(IDbContext dbContext, IProductAppRepository productAppRepository)
        {
            this.dbContext = dbContext;
            this.productAppRepository = productAppRepository;
        }

        async Task IModifyProductService.Execute(ModifyProductCommand data, CancellationToken cancellationToken)
        {
            var notification = Notification.New();
            var product = await productAppRepository.GetById(data.Id, cancellationToken);
            var handlerModifyProduct = HandlerModifyProduct.New(data, notification);

            product.Handler(handlerModifyProduct);

            notification.ThrowExceptionIfError();

            productAppRepository.Update(product);

            var ddd = await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
