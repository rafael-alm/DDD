using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectName.application.input.services.product.interfaces;
using projectName.domain.aggregates.product;
using projectName.domain.shared.seedWork.exceptions;
using projectName.infra.data.input.entityTypeConfiguration.models;

namespace projectName.infra.data.input.aggregates
{
    public class ProductRepository : IProductAppRepository
    {
        protected readonly ContextProductManagement context;
        private DbSet<ProductModel> products;
        private readonly IMapper mapper;


        public ProductRepository(ContextProductManagement context, IMapper mapper)
        {
            this.context = context;
            this.products = context.Set<ProductModel>();
            this.mapper = mapper;
        }

        async Task IProductAppRepository.Add(Product entity, CancellationToken cancellationToken)
            => await products.AddAsync(mapper.Map<ProductModel>(entity), cancellationToken);

        async Task<Product> IProductAppRepository.GetById(Guid id, CancellationToken cancellationToken)
        {
            var productModel = await products.FirstOrDefaultAsync(x => x.Id == id);

            NotFoundException.ThrowIfNull(productModel);

            return mapper.Map<Product>(productModel);
        }

        void IProductAppRepository.Update(Product entity)
        {
            var productModel = products.Single(x => x.Id == entity.Id);
            productModel = mapper.Map(entity, productModel);

            products.Update(productModel);
            context.Entry(productModel)
                   .Property(nameof(productModel.Code))
                   .IsModified = false;
        }
    }
}
