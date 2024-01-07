using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using projectName.application.input.seedWork.repository;
using projectName.infra.data.input;

namespace projectName.tests.integration.common
{
    public class TestContextProductManagement : ContextProductManagement, IDbContext
    {
        public static TestContextProductManagement New()
            => new TestContextProductManagement();

        public TestContextProductManagement()
            : base(new DbContextOptionsBuilder<TestContextProductManagement>()
                .UseInMemoryDatabase("TestContextProductManagement")
                .UseInternalServiceProvider(new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider())
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging()
                .Options)
        {
        }
    }
}
