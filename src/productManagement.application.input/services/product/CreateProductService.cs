using productManagement.application.input.services.product.dto;
using productManagement.application.input.services.product.interfaces;
using productManagement.application.input.seedWork.repository;
using productManagement.domain.aggregates.product.commands;
using productManagement.domain.aggregates.product.handlers;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.application.input.services.product
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
