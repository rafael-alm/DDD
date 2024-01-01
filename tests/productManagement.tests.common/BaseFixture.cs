﻿using Bogus;

namespace productManagement.tests.common
{
    public abstract class BaseFixture
    {
        protected BaseFixture()
            => Faker = new Faker("pt_BR");

        protected Faker Faker { get; set; }

    }
}
