using AutoMapper;
using FluentAssertions;
using productManagement.application.input.seedWork.repository;
using productManagement.application.input.services.product;
using productManagement.application.input.services.product.dto;
using productManagement.application.input.services.product.interfaces;
using productManagement.domain.aggregates.product.validations;
using productManagement.domain.shared.enumeration;
using productManagement.domain.shared.seedWork.exceptions;
using productManagement.infra.data.input.aggregates;
using productManagement.tests.common.fixture;
using productManagement.tests.integration.common;

namespace productManagement.tests.integration.application.services.product
{
    public class InactivateProductTest
    {
        private readonly ProductFixture productFixture;
        private readonly TestContextProductManagement dbContext;
        private readonly IMapper mapper;

        public InactivateProductTest(TestContextProductManagement dbContext, IMapper mapper)
        {
            productFixture = new ProductFixture();
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [Fact(DisplayName = nameof(ValidInactivateProductTest))]
        [Trait("Apliccation", "Service - Product")]
        public async void ValidInactivateProductTest()
        {
            IProductAppRepository repsitory = new ProductRepository(dbContext, mapper);
            ICreateProductService createProductService = new CreateProductService(dbContext, repsitory);

            var returnProductCreation = await createProductService.Execute(productFixture.GetValidCreateProductCommandWithAllData(), CancellationToken.None);

            var productCreated = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            IInactivateProductService inactivateProductService = new InactivateProductService(dbContext, repsitory);

            await inactivateProductService.Execute(returnProductCreation.Id, CancellationToken.None);

            var productNow = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            productNow.Should().NotBeNull();
            productNow.Id.Should().Be(productCreated.Id);
            productNow.Description.Should().Be(productCreated.Description);
            productNow.SupplierId.Should().Be(productCreated.SupplierId);
            productNow.ManufacturingDate.Should().Be(productCreated.ManufacturingDate);
            productNow.ExpirationDate.Should().Be(productCreated.ExpirationDate);
            productNow.Status.Should().Be(StatusEntityEnum.Inactive);
        }

        [Fact(DisplayName = nameof(InactivateExpectingEntityValidationException))]
        [Trait("Apliccation", "Service - Product")]
        public async void InactivateExpectingEntityValidationException()
        {
            IProductAppRepository repsitory = new ProductRepository(dbContext, mapper);

            ICreateProductService createProductService = new CreateProductService(dbContext, repsitory);
            var returnProductCreation = await createProductService.Execute(productFixture.GetValidCreateProductCommandWithAllData(), CancellationToken.None);
            var productCreated = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            IInactivateProductService inactivateProductService = new InactivateProductService(dbContext, repsitory);

            await inactivateProductService.Execute(productCreated.Id, CancellationToken.None);

            var productvvvvv = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            var task = async () => await inactivateProductService.Execute(productCreated.Id, CancellationToken.None);

            task.Should().ThrowAsync<EntityValidationException>().Result
                .Which.MessagesNotification.Should().Contain(ProductMessages.ProductIsAlreadyInactive);

            var productNow = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            productNow.Should().NotBeNull();
            productNow.Id.Should().Be(productCreated.Id);
            productNow.Description.Should().Be(productCreated.Description);
            productNow.SupplierId.Should().Be(productCreated.SupplierId);
            productNow.ManufacturingDate.Should().Be(productCreated.ManufacturingDate);
            productNow.ExpirationDate.Should().Be(productCreated.ExpirationDate);
            productNow.Status.Should().Be(StatusEntityEnum.Inactive);
        }
    }
}
