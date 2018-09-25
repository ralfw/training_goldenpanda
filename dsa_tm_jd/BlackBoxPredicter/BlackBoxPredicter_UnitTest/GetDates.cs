using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace BlackBoxPredicter_UnitTest
{
    [TestFixture]
    public class DatesProvider
    {
        [Test]
        public void GetDates()
        {
            var result = BlackBoxPredicter.DatesProvider.GetDates();

            result.Count.Should().Be(8);
            result[0].ShouldBeEquivalentTo(new Tuple<DateTime,DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")));
        }

        
    }
}
