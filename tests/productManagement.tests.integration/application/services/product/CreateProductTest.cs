using AutoMapper;
using FluentAssertions;
using productManagement.application.input.services.product;
using productManagement.application.input.services.product.interfaces;
using productManagement.domain.aggregates.product.commands;
using productManagement.domain.aggregates.product.validations;
using productManagement.domain.shared.enumeration;
using productManagement.domain.shared.seedWork.exceptions;
using productManagement.infra.data.input.aggregates;
using productManagement.tests.common.fixture;
using productManagement.tests.integration.common;

namespace productManagement.tests.integration.application.services.product
{
    public class CreateProductTest
    {
        private readonly ProductFixture productFixture;
        private readonly IMapper mapper;

        public CreateProductTest(IMapper mapper)
        {
            productFixture = new ProductFixture();
            this.mapper = mapper;
        }

        [Fact(DisplayName = nameof(ValidCreationOfBasicData))]
        [Trait("Apliccation", "Service - Product")]
        public async void ValidCreationOfBasicData()
        {
            var dbContex = TestContextProductManagement.New();
            var validCreateProductCommand = productFixture.GetValidCreateProductCommandWithBasicData();
            IProductAppRepository repsitory = new ProductRepository(dbContex, mapper);
            ICreateProductService createProductService = new CreateProductService(dbContex, repsitory);

            var returnProductCreation = await createProductService.Execute(validCreateProductCommand, CancellationToken.None);

            returnProductCreation.Id.Should().NotBeEmpty();
            returnProductCreation.Id.Should().NotBe("00000000-0000-0000-0000-000000000000");

            var productNow = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            productNow.Should().NotBeNull();
            productNow.Id.Should().Be(returnProductCreation.Id);
            productNow.Description.Should().Be(validCreateProductCommand.Description);
            productNow.SupplierId.Should().Be(validCreateProductCommand.SupplierId);
            productNow.ManufacturingDate.Should().Be(validCreateProductCommand.ManufacturingDate);
            productNow.ExpirationDate.Should().Be(validCreateProductCommand.ExpirationDate);
            productNow.Status.Should().Be(StatusEntityEnum.Active);
        }

        [Fact(DisplayName = nameof(ValidCreationOfAllData))]
        [Trait("Apliccation", "Service - Product")]
        public async void ValidCreationOfAllData()
        {
            var dbContex = TestContextProductManagement.New();
            var validCreateProductCommand = productFixture.GetValidCreateProductCommandWithAllData();
            var returnProductCreation = await productFixture.CreateProductInDBContext(dbContex, mapper, validCreateProductCommand);
            IProductAppRepository repsitory = new ProductRepository(dbContex, mapper);

            returnProductCreation.Id.Should().NotBeEmpty();
            returnProductCreation.Id.Should().NotBe("00000000-0000-0000-0000-000000000000");

            var productNow = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            productNow.Should().NotBeNull();
            productNow.Id.Should().Be(returnProductCreation.Id);
            productNow.Description.Should().Be(validCreateProductCommand.Description);
            productNow.SupplierId.Should().Be(validCreateProductCommand.SupplierId);
            productNow.ManufacturingDate.Should().Be(validCreateProductCommand.ManufacturingDate);
            productNow.ExpirationDate.Should().Be(validCreateProductCommand.ExpirationDate);
            productNow.Status.Should().Be(StatusEntityEnum.Active);
        }

        [Fact(DisplayName = nameof(CreationExpectingEntityValidationException))]
        [Trait("Apliccation", "Service - Product")]
        public void CreationExpectingEntityValidationException()
        {
            var dbContex = TestContextProductManagement.New();
            var repsitory = new ProductRepository(dbContex, mapper);
            ICreateProductService createProductService = new CreateProductService(dbContex, repsitory);

            var task = async () => await createProductService.Execute(new CreateProductCommand(), CancellationToken.None);

            task.Should().ThrowAsync<EntityValidationException>().Result
                .Which.MessagesNotification.Should().Contain(ProductMessages.DescriptionIsRequired);

            task = async () => await createProductService.Execute(productFixture.GetInvalidCreateProductCommandWithAllData(), CancellationToken.None);

            var messagens = new List<ProductMessages>()
            {
                ProductMessages.DescriptionMustHaveAMaximumOf250Characters,
                ProductMessages.ExpirationDateCannotBeLessThanTheManufacturingDate
            };

            task.Should().ThrowAsync<EntityValidationException>().Result
                .Which.MessagesNotification.Should().Contain(messagens);
        }
    }
}
