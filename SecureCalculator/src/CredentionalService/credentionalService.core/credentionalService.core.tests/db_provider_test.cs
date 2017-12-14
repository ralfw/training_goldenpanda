using credentionalService.core.adapter;
using FluentAssertions;
using NUnit.Framework;
using sc.contracts;

namespace credentionalService.core.tests
{
    [TestFixture]
    public class db_provider_test
    {
        [Test, Ignore]
        public void ShouldInsertUserAndPermissions()
        {
            db_provider dbProvider = new db_provider();

            dbProvider.insert_user("email","hash",new []{Permissions.Add, Permissions.Divide, });
            var result = dbProvider.get_user_info("email");
            result.user_email.Should().Be("email");
            result.password_hash.Should().Be("hash");
            result.permissions.Permissions.Should().ContainInOrder(new[] {Permissions.Add, Permissions.Divide,});

        }
    }
}