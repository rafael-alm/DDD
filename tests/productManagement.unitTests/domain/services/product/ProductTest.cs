using AutoMapper;
using FluentAssertions;
using productManagement.domain.aggregates.product.commands;
using productManagement.domain.aggregates.product.handlers;
using productManagement.domain.aggregates.product.validations;
using productManagement.domain.shared.enumeration;
using productManagement.domain.shared.seedWork.notification;
using productManagement.tests.common.fixture;

namespace productManagement.tests.unit.domain.aggregates.product
{
    [Collection(nameof(ProductFixture))]
    public class ProductTest
    {
        private readonly ProductFixture productFixture;

        public ProductTest()
        {
            productFixture = new ProductFixture();
        }

        [Fact(DisplayName = nameof(ValidCreationOfBasicData))]
        [Trait("Domain", "Aggregates - Product")]
        public void ValidCreationOfBasicData()
        {
            var notification = Notification.New();
            var validCreateProductCommand = productFixture.GetValidCreateProductCommandWithBasicData();
            var productValid = HandlerCreateProduct.New(validCreateProductCommand, notification)
                                                   .Execute();

            productValid.Should().NotBeNull();
            productValid.Id.Should().NotBeEmpty();
            productValid.Id.Should().NotBe("00000000-0000-0000-0000-000000000000");
            productValid.Description.Should().Be(validCreateProductCommand.Description);
            productValid.ExpirationDate.Should().Be(validCreateProductCommand.ExpirationDate);
            productValid.ManufacturingDate.Should().Be(validCreateProductCommand.ManufacturingDate);
            productValid.SupplierId.Should().Be(validCreateProductCommand.SupplierId);
            productValid.Code.Should().Be(0);
            productValid.Status.Should().Be(StatusEntityEnum.Active);

            notification.HasError.Should().BeFalse();
        }

        [Fact(DisplayName = nameof(ValidCreationOfAllData))]
        [Trait("Domain", "Aggregates - Product")]
        public void ValidCreationOfAllData()
        {
            var notification = Notification.New();
            var validCreateProductCommand = productFixture.GetValidCreateProductCommandWithAllData();
            var productValid = HandlerCreateProduct.New(validCreateProductCommand, notification)
                                                   .Execute();

            productValid.Should().NotBeNull();
            productValid.Id.Should().NotBeEmpty();
            productValid.Id.Should().NotBe("00000000-0000-0000-0000-000000000000");
            productValid.Description.Should().Be(validCreateProductCommand.Description);
            productValid.ExpirationDate.Should().Be(validCreateProductCommand.ExpirationDate);
            productValid.ManufacturingDate.Should().Be(validCreateProductCommand.ManufacturingDate);
            productValid.SupplierId.Should().Be(validCreateProductCommand.SupplierId);
            productValid.Code.Should().Be(0);
            productValid.Status.Should().Be(StatusEntityEnum.Active);

            notification.HasError.Should().BeFalse();
        }

        [Theory(DisplayName = nameof(CreationWithMandatoryDescriptionError))]
        [Trait("Domain", "Aggregates - Product")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void CreationWithMandatoryDescriptionError(string description)
        {
            var notification = Notification.New();
            var createProductCommand = new CreateProductCommand { Description = description };
            var product = HandlerCreateProduct.New(createProductCommand, notification)
                                              .Execute();

            product.Should().BeNull();
            notification.HasError.Should().BeTrue();
            notification.Errors.Count.Should().Be(1);
            notification.Errors.Single().Should().Be(ProductMessages.DescriptionIsRequired);
        }

        [Fact(DisplayName = nameof(CreationWithDescriptioErrorGreaterThan250Characters))]
        [Trait("Domain", "Aggregates - Product")]
        public void CreationWithDescriptioErrorGreaterThan250Characters()
        {
            var invalidDescription = productFixture.GetInvalidDescription();
            var notification = Notification.New();
            var createProductCommand = new CreateProductCommand { Description = invalidDescription };
            var product = HandlerCreateProduct.New(createProductCommand, notification)
                                              .Execute();

            product.Should().BeNull();
            notification.HasError.Should().BeTrue();
            notification.Errors.Count.Should().Be(1);
            notification.Errors.Single().Should().Be(ProductMessages.DescriptionMustHaveAMaximumOf250Characters);
        }

        [Fact(DisplayName = nameof(CreationWithAnErrorOfExpirationDateLessThanToTheManufacturingDate))]
        [Trait("Domain", "Aggregates - Product")]
        public void CreationWithAnErrorOfExpirationDateLessThanToTheManufacturingDate()
        {
            var dateNow = DateOnly.FromDateTime(DateTime.Now);
            var notification = Notification.New();
            var createProductCommand = new CreateProductCommand
            {
                Description = productFixture.GetValidDescription(),
                ManufacturingDate = dateNow,
                ExpirationDate = dateNow.AddDays(-1)
            };

            var product = HandlerCreateProduct.New(createProductCommand, notification)
                                              .Execute();

            product.Should().BeNull();
            notification.HasError.Should().BeTrue();
            notification.Errors.Count.Should().Be(1);
            notification.Errors.Single().Should().Be(ProductMessages.ExpirationDateCannotBeLessThanTheManufacturingDate);
        }

        [Fact(DisplayName = nameof(CreationWithAnErrorOfExpirationDateEqualThanToTheManufacturingDate))]
        [Trait("Domain", "Aggregates - Product")]
        public void CreationWithAnErrorOfExpirationDateEqualThanToTheManufacturingDate()
        {
            var dateNow = DateOnly.FromDateTime(DateTime.Now);
            var notification = Notification.New();
            var createProductCommand = new CreateProductCommand
            {
                Description = productFixture.GetValidDescription(),
                ManufacturingDate = dateNow,
                ExpirationDate = dateNow
            };

            var product = HandlerCreateProduct.New(createProductCommand, notification)
                                              .Execute();

            product.Should().BeNull();
            notification.HasError.Should().BeTrue();
            notification.Errors.Count.Should().Be(1);
            notification.Errors.Single().Should().Be(ProductMessages.ExpirationDateCannotBeLessThanTheManufacturingDate);
        }

        [Fact(DisplayName = nameof(ValidModificationOfAllData))]
        [Trait("Domain", "Aggregates - Product")]
        public void ValidModificationOfAllData()
        {
            var notification = Notification.New();
            var productValid = productFixture.GetValidProductWithBasicData();
            var validModifyProductCommand = productFixture.GetValidModifyProductCommandWithAllData(productValid.Id);
            var handlerModify = HandlerModifyProduct.New(validModifyProductCommand, notification);

            productValid.Handler(handlerModify);

            productValid.Id.Should().Be(validModifyProductCommand.Id);
            productValid.Description.Should().Be(validModifyProductCommand.Description);
            productValid.ExpirationDate.Should().Be(validModifyProductCommand.ExpirationDate);
            productValid.ManufacturingDate.Should().Be(validModifyProductCommand.ManufacturingDate);
            productValid.SupplierId.Should().Be(validModifyProductCommand.SupplierId);
            productValid.Code.Should().Be(0);
            productValid.Status.Should().Be(StatusEntityEnum.Active);

            notification.HasError.Should().BeFalse();
        }

        [Theory(DisplayName = nameof(ModificationWithMandatoryDescriptionError))]
        [Trait("Domain", "Aggregates - Product")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void ModificationWithMandatoryDescriptionError(string invalidDescription)
        {
            var notification = Notification.New();
            var productValid = productFixture.GetValidProductWithBasicData();
            var modifyProductCommand = new ModifyProductCommand { Id = productValid.Id, Description = invalidDescription };
            var handlerModify = HandlerModifyProduct.New(modifyProductCommand, notification);

            productValid.Handler(handlerModify);

            notification.HasError.Should().BeTrue();
            notification.Errors.Count.Should().Be(1);
            notification.Errors.Single().Should().Be(ProductMessages.DescriptionIsRequired);
        }

        [Fact(DisplayName = nameof(ModificationWithDescriptioErrorGreaterThan250Characters))]
        [Trait("Domain", "Aggregates - Product")]
        public void ModificationWithDescriptioErrorGreaterThan250Characters()
        {
            var invalidDescription = productFixture.GetInvalidDescription();
            var notification = Notification.New();
            var productValid = productFixture.GetValidProductWithBasicData();
            var modifyProductCommand = new ModifyProductCommand { Id = productValid.Id, Description = invalidDescription };
            var handlerModify = HandlerModifyProduct.New(modifyProductCommand, notification);

            productValid.Handler(handlerModify);

            notification.HasError.Should().BeTrue();
            notification.Errors.Count.Should().Be(1);
            notification.Errors.Single().Should().Be(ProductMessages.DescriptionMustHaveAMaximumOf250Characters);
        }

        [Fact(DisplayName = nameof(ModificationWithAnErrorOfExpirationDateLessThanToTheManufacturingDate))]
        [Trait("Domain", "Aggregates - Product")]
        public void ModificationWithAnErrorOfExpirationDateLessThanToTheManufacturingDate()
        {
            var dateNow = DateOnly.FromDateTime(DateTime.Now);

            var notification = Notification.New();
            var productValid = productFixture.GetValidProductWithBasicData();
            var modifyProductCommand = new ModifyProductCommand
            {
                Id = productValid.Id,
                Description = productFixture.GetValidDescription(),
                ManufacturingDate = dateNow,
                ExpirationDate = dateNow.AddDays(-1)
            };
            var handlerModify = HandlerModifyProduct.New(modifyProductCommand, notification);

            productValid.Handler(handlerModify);

            notification.HasError.Should().BeTrue();
            notification.Errors.Count.Should().Be(1);
            notification.Errors.Single().Should().Be(ProductMessages.ExpirationDateCannotBeLessThanTheManufacturingDate);
        }

        [Fact(DisplayName = nameof(ModificationWithAnErrorOfExpirationDateEqualThanToTheManufacturingDate))]
        [Trait("Domain", "Aggregates - Product")]
        public void ModificationWithAnErrorOfExpirationDateEqualThanToTheManufacturingDate()
        {
            var dateNow = DateOnly.FromDateTime(DateTime.Now);

            var notification = Notification.New();
            var productValid = productFixture.GetValidProductWithBasicData();
            var modifyProductCommand = new ModifyProductCommand
            {
                Id = productValid.Id,
                Description = productFixture.GetValidDescription(),
                ManufacturingDate = dateNow,
                ExpirationDate = dateNow
            };
            var handlerModify = HandlerModifyProduct.New(modifyProductCommand, notification);

            productValid.Handler(handlerModify);

            notification.HasError.Should().BeTrue();
            notification.Errors.Count.Should().Be(1);
            notification.Errors.Single().Should().Be(ProductMessages.ExpirationDateCannotBeLessThanTheManufacturingDate);
        }

        [Fact(DisplayName = nameof(ValidInactivate))]
        [Trait("Domain", "Aggregates - Product")]
        public void ValidInactivate()
        {
            var notification = Notification.New();
            var productValid = productFixture.GetValidProductWithBasicData();
            var handlerInactivate = HandlerInactivateProduct.New(notification);

            productValid.Handler(handlerInactivate);

            productValid.Status.Should().Be(StatusEntityEnum.Inactive);
            notification.HasError.Should().BeFalse();
            notification.Errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = nameof(InvalidInactivation))]
        [Trait("Domain", "Aggregates - Product")]
        public void InvalidInactivation()
        {
            var notification = Notification.New();
            var productValid = productFixture.GetValidProductWithBasicData();
            var handlerInactivate = HandlerInactivateProduct.New(notification);

            productValid.Handler(handlerInactivate, handlerInactivate);

            notification.HasError.Should().BeTrue();
            notification.Errors.Count.Should().Be(1);
            notification.Errors.Single().Should().Be(ProductMessages.ProductIsAlreadyInactive);
        }
    }
}
