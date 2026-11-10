using AutoMapper;
using Microsoft.Extensions.Logging;
using Pdc.Application.Mappings;

namespace Pdc.Tests.Mapping;

internal class DTOMappingTests
{

    IMapper _mapper;
    [SetUp]
    public void Setup()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.LicenseKey = Environment.GetEnvironmentVariable("AutoMapper:LicenseKey");
            cfg.AddProfile<MappingProfile>();
        }, LoggerFactory.Create(_ => { })).CreateMapper();
    }

    [Test]
    public void AssertMapper()
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
