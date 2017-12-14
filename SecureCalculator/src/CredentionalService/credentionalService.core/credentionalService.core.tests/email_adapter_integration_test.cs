using System;
using credentionalService.core.adapter;
using FluentAssertions;
using NUnit.Framework;

namespace credentionalService.core.tests
{
    [TestFixture]
    public class email_adapter_integration_test
    {
        [Test]
        public void ShouldThrowExceptionIfAddressInvalid()
        {
            var mail = new mailgun_adapter();

            Action act = () => { mail.NotifyUser("asdfwagocom", "Welcome new user", "pw"); };

            act.ShouldThrow<Exception>();
        }

        [Test]
        public void ShouldSendMail()
        {
            var mail = new mailgun_adapter();

            Action act = () => { mail.NotifyUser("nikolai.falke@wago.com", "Welcome new user", "pw"); };

            act.ShouldNotThrow<Exception>();
        }
    }
}