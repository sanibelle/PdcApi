using FluentAssertions;
using NUnit.Framework;
using Pdc.Application.DTOS;
using Pdc.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Pdc.E2ETests
{
    [TestFixture]
    public class ProgramOfStudyApiTests : ApiTestBase
    {
        [Test]
        public async Task GivenSeededPrograms_WhenGetAllProgramsOfStudy_ThenShouldReturnSeededPrograms()
        {
            // Act
            var response = await _client.GetAsync("/api/programofstudy");

            // Assert
            response.EnsureSuccessStatusCode();
            var programs = await response.Content.ReadFromJsonAsync<List<ProgramOfStudyDTO>>();

            Assert.That(programs, Is.Not.Null);
            Assert.That(programs.Count, Is.GreaterThan(0));
            Assert.That(programs.Any(p => p.Code == "TEST.123"), Is.True);
        }

        [Test]
        public async Task GivenNewProgram_WhenCreateProgramOfStudy_ThenShouldAddNewProgram()
        {
            // Arrange
            CreateProgramOfStudyDTO newProgram = new CreateProgramOfStudyDTO()
            {
                Code = "420.B0",
                Name = "Techniques de l'informatique",
                Sanction = Pdc.Domain.Enums.SanctionType.DEC,
                MonthsDuration = 36,
                SpecificDurationHours = 2010,
                TotalDurationHours = 5730,
                PublishedOn = DateOnly.FromDateTime(DateTime.Now),
                OptionnalUnits = new Units(16, 2, 3),
                SpecificUnits = new Units(26, 2, 3)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/programofstudy", newProgram);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var createdProgram = await response.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();

            // Verify it was added to the database
            var getResponse = await _client.GetAsync($"/api/programofstudy/{createdProgram.Id}");
            getResponse.EnsureSuccessStatusCode();
            var program = await getResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();

            createdProgram.Should().BeEquivalentTo(program, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task GivenExistingProgram_WhenDeleteProgramOfStudy_ThenShouldRemoveProgram()
        {
            // Arrange
            CreateProgramOfStudyDTO newProgram = new CreateProgramOfStudyDTO()
            {
                Code = "420.B0",
                Name = "Techniques de l'informatique",
                Sanction = Pdc.Domain.Enums.SanctionType.DEC,
                MonthsDuration = 36,
                SpecificDurationHours = 2010,
                TotalDurationHours = 5730,
                PublishedOn = DateOnly.FromDateTime(DateTime.Now),
                OptionnalUnits = new Units(16, 2, 3),
                SpecificUnits = new Units(26, 2, 3)
            };

            // Act - Create the program
            var createResponse = await _client.PostAsJsonAsync("/api/programofstudy", newProgram);
            createResponse.EnsureSuccessStatusCode();
            var createdProgram = await createResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();

            // Act - Delete the program
            var deleteResponse = await _client.DeleteAsync($"/api/programofstudy/{createdProgram.Id}");
            deleteResponse.EnsureSuccessStatusCode();

            // Assert - Verify the program was deleted
            var getResponse = await _client.GetAsync($"/api/programofstudy/{createdProgram.Id}");
            Assert.That(getResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }

        [Test]
        public async Task GivenExistingProgram_WhenUpdateProgramOfStudy_ThenShouldUpdateProgram()
        {
            // Arrange
            CreateProgramOfStudyDTO newProgram = new CreateProgramOfStudyDTO()
            {
                Code = "420.B1",
                Name = "Techniques de l'informatique intensive",
                Sanction = Pdc.Domain.Enums.SanctionType.DEC,
                MonthsDuration = 36,
                SpecificDurationHours = 2010,
                TotalDurationHours = 5730,
                PublishedOn = DateOnly.FromDateTime(DateTime.Now),
                OptionnalUnits = new Units(16, 2, 3),
                SpecificUnits = new Units(26, 2, 3)
            };

            ProgramOfStudyDTO updatedProgramData = new ProgramOfStudyDTO()
            {
                Id = new Guid(),
                Code = "421.B0",
                Name = "Techniques de l'informatique 2.0",
                Sanction = Pdc.Domain.Enums.SanctionType.AEC,
                MonthsDuration = 35,
                SpecificDurationHours = 53,
                TotalDurationHours = 35,
                PublishedOn = DateOnly.FromDateTime(DateTime.Now),
                OptionnalUnits = new Units(16, 2, 3),
                SpecificUnits = new Units(26, 2, 3),
                ComplementaryUnits = new Units(0),
                GeneralUnits = new Units(0)
            };

            // Act - Create the program
            var createResponse = await _client.PostAsJsonAsync("/api/programofstudy", newProgram);
            createResponse.EnsureSuccessStatusCode();
            var createdProgram = await createResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();

            // Act - Uodate the program
            updatedProgramData.Id = createdProgram.Id;
            var updateResponse = await _client.PutAsJsonAsync($"/api/programofstudy/{updatedProgramData.Id}", updatedProgramData);
            updateResponse.EnsureSuccessStatusCode();
            var updatedProgram = await updateResponse.Content.ReadFromJsonAsync<ProgramOfStudyDTO>();


            updatedProgram.Should().NotBeEquivalentTo(createdProgram, options =>
                options.ExcludingMissingMembers());
        }

    }
}