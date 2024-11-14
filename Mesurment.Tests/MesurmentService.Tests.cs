using Moq;
using Xunit.Abstractions;
using Mesurment.Service;
using Mesurment.Request_Responce;
using Model;
using Mesurment.Controller;
using FeatureHub;

namespace Mesurment.Tests;

public class MesurmentTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    public MesurmentTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async void Test1()
    {
        // Arrange
        var mock = new Mock<IMesurmentService>();

        var GeneralResponce = new GeneralResponce(200, "Success");

        mock.Setup(client => client.CreateMeasurements(It.IsAny<Measurements>())).ReturnsAsync(GeneralResponce);

        var featureServiceMock = new Mock<FeatureService>();

        featureServiceMock.Setup(client => client.FeatureFlagChecker(It.IsAny<Features>())).Returns(true);

        var mesurmentController = new MesurmentController(mock.Object, featureServiceMock.Object);

        // Act
        var result = await mesurmentController.CreateMeasurements(new Measurements()
        {
            Datetime = DateTime.Now,
            Systolic = 120,
            Diastolic = 80,
            PatientSSN = "123456789",
        });

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }
}