using AutoMapper;
using FluentAssertions;
using productManagement.application.input.services.product;
using productManagement.application.input.services.product.dto;
using productManagement.application.input.services.product.interfaces;
using productManagement.domain.aggregates.product;
using productManagement.domain.aggregates.product.commands;
using productManagement.domain.aggregates.product.validations;
using productManagement.domain.shared.enumeration;
using productManagement.domain.shared.seedWork.exceptions;
using productManagement.infra.data.input.aggregates;
using productManagement.infra.data.input.seedWork;
using productManagement.tests.common.fixture;
using productManagement.tests.integration.common;

namespace productManagement.tests.integration.application.services.product
{
    public class ModifyProductTest
    {
        private readonly ProductFixture productFixture;
        private readonly IMapper mapper;

        public ModifyProductTest(IMapper mapper)
        {
            productFixture = new ProductFixture();
            this.mapper = mapper;
        }

        [Fact(DisplayName = nameof(ValidModification))]
        [Trait("Apliccation", "Service - Product")]
        public async void ValidModification()
        {
            var dbContex = TestContextProductManagement.New();

            IProductAppRepository repsitory = new ProductRepository(dbContex, mapper);
            ICreateProductService createProductService = new CreateProductService(dbContex, repsitory);

            var returnProductCreation = await createProductService.Execute(productFixture.GetValidCreateProductCommandWithAllData(), CancellationToken.None);
            var modifyProductCommand = productFixture.GetValidModifyProductCommandWithAllData(returnProductCreation.Id);

            IModifyProductService modifyProductService = new ModifyProductService(dbContex, repsitory);

            await modifyProductService.Execute(modifyProductCommand, CancellationToken.None);

            var productNow = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            productNow.Should().NotBeNull();
            productNow.Id.Should().Be(modifyProductCommand.Id);
            productNow.Description.Should().Be(modifyProductCommand.Description);
            productNow.SupplierId.Should().Be(modifyProductCommand.SupplierId);
            productNow.ManufacturingDate.Should().Be(modifyProductCommand.ManufacturingDate);
            productNow.ExpirationDate.Should().Be(modifyProductCommand.ExpirationDate);
            productNow.Status.Should().Be(StatusEntityEnum.Active);
        }

        [Fact(DisplayName = nameof(ModificationExpectingEntityValidationException))]
        [Trait("Apliccation", "Service - Product")]
        public async void ModificationExpectingEntityValidationException()
        {
            var dbContex = TestContextProductManagement.New();

            IProductAppRepository repsitory = new ProductRepository(dbContex, mapper);
            ICreateProductService createProductService = new CreateProductService(dbContex, repsitory);

            var returnProductCreation = await createProductService.Execute(productFixture.GetValidCreateProductCommandWithAllData(), CancellationToken.None);

            var productCreated = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            IModifyProductService modifyProductService = new ModifyProductService(dbContex, repsitory);

            var task = async () => await modifyProductService.Execute(new ModifyProductCommand { Id = returnProductCreation.Id }, CancellationToken.None);
            task.Should().ThrowAsync<EntityValidationException>().Result
                .Which.MessagesNotification.Should().Contain(ProductMessages.DescriptionIsRequired);


            var invalidModifyProductCommand = productFixture.GetInvalidModifyProductCommandWithAllData(returnProductCreation.Id);
            task = async () => await modifyProductService.Execute(invalidModifyProductCommand, CancellationToken.None);

            var messagens = new List<ProductMessages>()
            {
                ProductMessages.DescriptionMustHaveAMaximumOf250Characters,
                ProductMessages.ExpirationDateCannotBeLessThanTheManufacturingDate
            };

            task.Should().ThrowAsync<EntityValidationException>().Result
                .Which.MessagesNotification.Should().Contain(messagens);

            var productNow = await repsitory.GetById(returnProductCreation.Id, CancellationToken.None);

            productNow.Should().NotBeNull();
            productNow.Id.Should().Be(productCreated.Id);
            productNow.Description.Should().Be(productCreated.Description);
            productNow.SupplierId.Should().Be(productCreated.SupplierId);
            productNow.ManufacturingDate.Should().Be(productCreated.ManufacturingDate);
            productNow.ExpirationDate.Should().Be(productCreated.ExpirationDate);
            productNow.Status.Should().Be(StatusEntityEnum.Active);
        }
    }
}
