using System;
using NUnit.Framework;
using sc.contracts;

namespace CalculatorCredentialService
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void CallWithError()
        {
            var service = new CalculatorCredentialService();
            var error = string.Empty;
            PermissionSet ps = null;
            service.LogIn("test@mich_error.de","123456", set => { ps = set;}, s => { error = s;});

            Assert.True(error.Contains("test@mich_error.de"));
        }

        [Test]
        public void CallWithoutError()
        {
            var service = new CalculatorCredentialService();
            var error = string.Empty;
            PermissionSet ps = null;
            service.LogIn("test@mich.de", "123456", set => { ps = set; }, s => { error = s; });
            Assert.True(string.IsNullOrEmpty(error));
            Assert.True(ps.Permissions.Length==2);
        }
    }
}