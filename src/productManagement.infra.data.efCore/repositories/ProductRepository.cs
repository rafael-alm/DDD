using AutoMapper;
using Microsoft.EntityFrameworkCore;
using productManagement.application.input.seedWork.repository;
using productManagement.application.input.services.product.interfaces;
using productManagement.domain.aggregates.product;
using productManagement.domain.shared.seedWork.exceptions;
using productManagement.infra.data.input.entityTypeConfiguration.models;

namespace productManagement.infra.data.input.aggregates
{
    public class ProductRepository : IProductAppRepository
    {
        protected readonly ContextProductManagement context;
        private readonly IMapper mapper;

        private DbSet<ProductModel> products
            => context.Set<ProductModel>();

        public ProductRepository(IDbContext context, IMapper mapper)
        {
            this.context = context as ContextProductManagement;
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

        async void IProductAppRepository.Update(Product entity)
        {
            var productModel = await products.SingleAsync(x => x.Id == entity.Id);
            productModel = mapper.Map(entity, productModel);

            products.Update(productModel);
            context.Entry(productModel)
                   .Property(nameof(productModel.Code))
                   .IsModified = false;
        }
    }
}
