using System;
using System.ComponentModel;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._ConfigProvider
{
    [TestFixture]
    public class GetConfig
    {
        [Test]
        public void ShoudReturnValidConfigFromArguments()
        {
            var startDate = new DateTime(2000,1,1);
            var endDate = new DateTime(2000, 12, 31);
            var args = new []{startDate.ToString(CultureInfo.InvariantCulture), endDate.ToString(CultureInfo.InvariantCulture), "a" };
            var config = ConfigProvider.GetConfig(args);
            config.Should().NotBeNull();
            config.ProtocolFilePath.Should().Be("a");
            config.StartDate.Should().Be(startDate);
            config.EndDate.Should().Be(endDate);
        }

        [Test]
        public void ShoudThrowFormatExceptionForInvalidDateFormats()
        {
            var args = new[] { "a","b", "c"};
            var action = new Action(() => ConfigProvider.GetConfig(args));
            action.ShouldThrow<FormatException>();
        }

        [Test]
        public void ShoudThrowArgumentExceptionWhenStartDateGreaterEndDate()
        {
            var startDate = new DateTime(2002, 1, 1);
            var endDate = new DateTime(2000, 12, 31);
            var args = new[] { startDate.ToString(CultureInfo.InvariantCulture), endDate.ToString(CultureInfo.InvariantCulture), "a" }; 
            var action = new Action(() => ConfigProvider.GetConfig(args));
            action.ShouldThrow<ArgumentException>().WithMessage("StartDate should be less or equal EndDate");
        }


        [Test]
        public void ShouldThrowArgumentExceptionWhenLessArguments()
        {
            var args = new string [] { };
            var action = new Action(() => ConfigProvider.GetConfig(args));
            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ShouldThrowArgumentExceptionWhenMoreArguments()
        {
            var args = new [] {"a","b","c","d"};
            var action = new Action(() => ConfigProvider.GetConfig(args));
            action.ShouldThrow<ArgumentException>();
        }
    }
}