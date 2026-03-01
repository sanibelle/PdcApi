// TODO $$$$ Payer fluent assertion
using FluentAssertions;
using Pdc.Domain.DTOS.Common;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using System.Net.Http.Json;
using TestDataSeeder;
using TestDataSeeder.Builders.DTOS;

namespace Pdc.E2ETests;

[TestFixture]
public class ComplementaryInformation : ApiTestBase
{

    [Test]
    public async Task GivenComplementaryInformation_WhenCreatingComplementaryInformation_ThenShouldReturnCreatedComplementaryInformation()
    {
        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text")
            .Build();

        // Act - Creation
        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/complementaryInformation", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        ValidatedCreatedComplementaryInformation(complementaryInformationDTO, createdComplementaryInformation);
    }


    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingComplementaryInformation_ThenShouldReturnCreatedComplementaryInformation()
    {
        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text")
            .Build();

        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/complementaryInformation", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        createdComplementaryInformation.Should().NotBeNull();

        complementaryInformationDTO.Text = "This is the updated test text";
        var updateResponse = await _Client.PutAsJsonAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}", complementaryInformationDTO);

        // Act - Update
        updateResponse.EnsureSuccessStatusCode();
        var updatedComplementaryInformation = await updateResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        ValidatedUpdatedComplementaryInformation(complementaryInformationDTO, updatedComplementaryInformation);
    }


    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingMissingComplementaryInformation_ThenShouldReturn404()
    {
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("It never existed")
            .Build();
        var updateResponse = await _Client.PutAsJsonAsync($"/api/complementaryInformation/{new Guid()}", complementaryInformationDTO);

        // Act - Update
        updateResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingComplementaryInformationAsAdmin_ThenShouldSucceed()
    {
        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text of the simple user")
            .Build();

        SwitchUserRequesting(DataSeeder.SimpleUser);
        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/complementaryInformation", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        createdComplementaryInformation.Should().NotBeNull();


        // Act - Update with admin rights
        SwitchUserRequesting(DataSeeder.Admin);
        complementaryInformationDTO.Text = "This is the updated test text of the admin";
        var updateResponse = await _Client.PutAsJsonAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}", complementaryInformationDTO);
        updateResponse.EnsureSuccessStatusCode();
        var updatedComplementaryInformation = await updateResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        ValidatedUpdatedComplementaryInformation(complementaryInformationDTO, updatedComplementaryInformation);
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingComplementaryInformationAsAuthor_ThenShouldSucceed()
    {
        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text of the simple user")
            .Build();

        SwitchUserRequesting(DataSeeder.SimpleUser);
        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/complementaryInformation", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        createdComplementaryInformation.Should().NotBeNull();


        // Act - Update with admin rights
        complementaryInformationDTO.Text = "This is the updated test text of the creation user";
        var updateResponse = await _Client.PutAsJsonAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}", complementaryInformationDTO);
        updateResponse.EnsureSuccessStatusCode();
        var updatedComplementaryInformation = await updateResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        ValidatedUpdatedComplementaryInformation(complementaryInformationDTO, updatedComplementaryInformation);
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenUpdatingComplementaryInformationNotAsAuthor_ThenShouldFail()
    {
        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text of the simple user")
            .Build();

        SwitchUserRequesting(DataSeeder.Admin);
        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/complementaryInformation", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        createdComplementaryInformation.Should().NotBeNull();


        // Act - Update with another user
        SwitchUserRequesting(DataSeeder.SimpleUser);
        complementaryInformationDTO.Text = "This is the updated test text of the creation user";
        var updateResponse = await _Client.PutAsJsonAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}", complementaryInformationDTO);
        updateResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingComplementaryInformation_ThenShouldDeleteComplementaryInformation()
    {
        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text of the simple user")
            .Build();

        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/complementaryInformation", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        createdComplementaryInformation.Should().NotBeNull();


        // Act - Update with another user
        complementaryInformationDTO.Text = "This is the updated test text of the creation user";
        var deleteResponse = await _Client.DeleteAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}");
        deleteResponse.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingMissingComplementaryInformation_ThenShouldReturn404()
    {
        SwitchUserRequesting(DataSeeder.SimpleUser);

        // Act - Update with admin
        SwitchUserRequesting(DataSeeder.Admin);
        var deleteResponse = await _Client.DeleteAsync($"/api/complementaryInformation/{new Guid()}");
        deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }


    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingComplementaryInformationAsAdmin_ThenShouldSucceed()
    {

        SwitchUserRequesting(DataSeeder.SimpleUser);
        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text of the simple user")
            .Build();

        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/complementaryInformation", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        createdComplementaryInformation.Should().NotBeNull();


        // Act - Update with admin
        SwitchUserRequesting(DataSeeder.Admin);
        complementaryInformationDTO.Text = "This is the updated test text of the creation user";
        var deleteResponse = await _Client.DeleteAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}");
        deleteResponse.EnsureSuccessStatusCode();

        var getDeletedResponse = await _Client.DeleteAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}");
        getDeletedResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingComplementaryInformationAsAuthor_ThenShouldSucceed()
    {
        SwitchUserRequesting(DataSeeder.SimpleUser);

        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text of the simple user")
            .Build();

        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/complementaryInformation", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        createdComplementaryInformation.Should().NotBeNull();


        // Act - Update with same user
        complementaryInformationDTO.Text = "This is the updated test text of the creation user";
        var deleteResponse = await _Client.DeleteAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}");
        deleteResponse.EnsureSuccessStatusCode();

        var getDeletedResponse = await _Client.DeleteAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}");
        getDeletedResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GivenComplementaryInformation_WhenDeletingComplementaryInformationNotAsAuthor_ThenShouldFail()
    {
        CompetencyElementEntity competencyElement = DataSeeder.CompetencyEntity.CompetencyElements.First();
        Assert.That(competencyElement != null, "there should be at least one competency element seeded");
        ComplementaryInformationDTO complementaryInformationDTO = new ComplementaryInformationDTOBuilder()
            .WithText("This is the test text of the simple user")
            .Build();

        SwitchUserRequesting(DataSeeder.Admin);
        var createResponse = await _Client.PostAsJsonAsync($"/api/changeable/{competencyElement.Id}/complementaryInformation", complementaryInformationDTO);
        createResponse.EnsureSuccessStatusCode();
        var createdComplementaryInformation = await createResponse.Content.ReadFromJsonAsync<ComplementaryInformationDTO>();
        createdComplementaryInformation.Should().NotBeNull();


        // Act - Update with another user
        SwitchUserRequesting(DataSeeder.SimpleUser);
        complementaryInformationDTO.Text = "This is the updated test text of the creation user";
        var deleteResponse = await _Client.DeleteAsync($"/api/complementaryInformation/{createdComplementaryInformation.Id}");
        deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }

    [Test]
    [Ignore("TODO when the versinning will be implemented, complete this test.")]
    public async Task GivenComplementaryInformation_WhenUpdatingWithNewVersion_ThenShouldUpdateTheVersion()
    {
    }

    private static void ValidatedComplementaryInformation(ComplementaryInformationDTO? complementaryInformation)
    {
        complementaryInformation.Should().NotBeNull();
        complementaryInformation.CreatedOn.Should().NotBeNull();
        complementaryInformation.CreatedBy.Should().NotBeNull();
        complementaryInformation.Id.Should().NotBeNull();
        complementaryInformation.WrittenOnVersion.Should().NotBeNull();
    }

    private static void ValidatedUpdatedComplementaryInformation(ComplementaryInformationDTO complementaryInformationDTO, ComplementaryInformationDTO? updatedComplementaryInformation)
    {
        ValidatedComplementaryInformation(updatedComplementaryInformation);
        updatedComplementaryInformation.Should().BeEquivalentTo(complementaryInformationDTO, options =>
           options
           .Excluding(x => x.Id)
           .Excluding(x => x.WrittenOnVersion)
           .Excluding(x => x.CreatedOn)
           .Excluding(x => x.ModifiedOn)
           .Excluding(x => x.CreatedBy));

        updatedComplementaryInformation.ModifiedOn.Should().NotBeNull();
    }

    private static void ValidatedCreatedComplementaryInformation(ComplementaryInformationDTO complementaryInformationDTO, ComplementaryInformationDTO? createdComplementaryInformation)
    {
        ValidatedComplementaryInformation(createdComplementaryInformation);
        createdComplementaryInformation.Should().BeEquivalentTo(complementaryInformationDTO, options =>
           options
           .Excluding(x => x.Id)
           .Excluding(x => x.WrittenOnVersion)
           .Excluding(x => x.CreatedOn)
           .Excluding(x => x.CreatedBy));

        createdComplementaryInformation.ModifiedOn.Should().BeNull();
    }
}