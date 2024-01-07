using projectName.application.input.services.product.dto;
using projectName.application.input.services.product.interfaces;
using projectName.application.input.seedWork.repository;
using projectName.domain.aggregates.product.commands;
using projectName.domain.aggregates.product.handlers;
using projectName.domain.shared.seedWork.notification;

namespace projectName.application.input.services.product
{
    public sealed class CreateProductService : ICreateProductService
    {
        private readonly IDbContext dbContext;
        private readonly IProductAppRepository productAppRepository;

        public CreateProductService(IDbContext dbContext, IProductAppRepository productAppRepository)
        {
            this.dbContext = dbContext;
            this.productAppRepository = productAppRepository;
        }

        async Task<ReturnProductCreation> ICreateProductService.Execute(CreateProductCommand data, CancellationToken cancellationToken)
        {
            var notification = Notification.New();
            var handlerCreateProduct = HandlerCreateProduct.New(data, notification);
            var product = handlerCreateProduct.Execute();

            notification.ThrowExceptionIfError();

            await productAppRepository.Add(product!, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Commit();

            return new ReturnProductCreation(product!.Id);
        }
    }
}
