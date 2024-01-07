using projectName.application.input.services.product.interfaces;
using projectName.application.input.seedWork.repository;
using projectName.domain.aggregates.product.handlers;
using projectName.domain.shared.seedWork.notification;

namespace projectName.application.input.services.product
{
    public sealed class InactivateProductService : IInactivateProductService
    {
        private readonly IDbContext dbContext;
        private readonly IProductAppRepository productAppRepository;

        public InactivateProductService(IDbContext dbContext, IProductAppRepository productAppRepository)
        {
            this.dbContext = dbContext;
            this.productAppRepository = productAppRepository;
        }

        async Task IInactivateProductService.Execute(Guid id, CancellationToken cancellationToken)
        {
            var notification = Notification.New();
            var product = await productAppRepository.GetById(id, cancellationToken);
            var handlerInactivateProduct = HandlerInactivateProduct.New(notification);

            product.Handler(handlerInactivateProduct);

            notification.ThrowExceptionIfError();

            productAppRepository.Update(product);

            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Commit();
        }
    }
}
