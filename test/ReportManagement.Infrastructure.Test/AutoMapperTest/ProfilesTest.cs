using AutoMapper;
using NUnit.Framework;
using ReportManagement.Application.AutoMapper;

namespace ReportManagement.Infrastructure.Test.AutoMapperTest
{
    internal class ProfilesTest
    {
        [Test]
        public void WhenProfilesAreConfigured_ItShouldNotThrowException()
        {
            // Arrange
            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddMaps(typeof(Profiles).Assembly);
            });

            // Assert
            config.AssertConfigurationIsValid();
        }
    }
}
