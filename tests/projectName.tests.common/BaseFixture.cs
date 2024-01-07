using Bogus;

namespace projectName.tests.common
{
    public abstract class BaseFixture
    {
        protected BaseFixture()
            => Faker = new Faker("pt_BR");

        protected Faker Faker { get; set; }

    }
}
