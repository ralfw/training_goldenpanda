using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._Integrator
{
    [TestFixture]
    public class Convert
    {
        [Test]
        public void ShouldReturnAccountNumbers()
        {
            List<string> lines = new List<string>();
            lines.Add("    _  _     _  _  _  _  _  _ ");
            lines.Add("  I _I _II_II_ I_   II_II_II I");
            lines.Add("  II_  _I  I _II_I  II_I _II_I");
            lines.Add("                              ");
            lines.Add("    _  _     _  _  _  _  _  _ ");
            lines.Add("  I _I _II_II_ I_   II_II_II I");
            lines.Add("  II_  _I  I _II_I  II_I _II_I");
            lines.Add("                             ");

            var integrator = new BankOCR();
            var accountNumbers = integrator.Decode(lines);
            accountNumbers.Should().HaveCount(2);
            accountNumbers[0].Should().Be("1234567890");
            accountNumbers[1].Should().Be("1234567890");

        }
    }
}