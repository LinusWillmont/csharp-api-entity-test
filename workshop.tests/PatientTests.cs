using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using workshop.wwwapi.Models;

namespace workshop.tests;

public class PatientTests
{

    [Test]
    public async Task PatientEndpointStatus()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/surgery/patients");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task GetPatientsResponse()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        var expectedPayload = new List<Patient>()
            {
                new Patient { Id = 1, FullName = "Klas Bengtsson" },
                new Patient { Id = 2, FullName = "Kerstin Gunnarsson" }
            };

        // Act
        var response = await client.GetAsync("/surgery/patients");

        var responsePayload = await response.Content.ReadFromJsonAsync<IEnumerable<Patient>>();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

        //Assert.That(responsePayload.Result.Count(), Is.EqualTo(3));
        for (int i = 0; i < expectedPayload.Count; i++)
        {
            Assert.That(responsePayload.ElementAtOrDefault(i).Id, Is.EqualTo(expectedPayload[i].Id));
            Assert.That(responsePayload.ElementAtOrDefault(i).FullName, Is.EqualTo(expectedPayload[i].FullName));
        }
    }
}