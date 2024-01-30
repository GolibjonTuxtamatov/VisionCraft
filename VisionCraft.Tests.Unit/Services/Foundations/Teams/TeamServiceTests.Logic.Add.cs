using FluentAssertions;
using Force.DeepCloner;
using Moq;
using VisionCraft.Models.Teams;
using Xunit;

namespace VisionCraft.Tests.Unit.Services.Foundations.Teams
{
    public partial class TeamServiceTests
    {
        [Fact]
        public async Task ShouldAddTeamAsync()
        {
            //given
            Team randomTeam = CreateRandomTeam();
            Team inputTeam = randomTeam;
            Team storageTeam = inputTeam;
            Team expectedTeam = storageTeam.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertTeamAsync(inputTeam))
                    .ReturnsAsync(expectedTeam);

            //when
            Team actualTeam =
                await this.teamService.AddTeamAsync(inputTeam);

            //then
            actualTeam.Should().BeEquivalentTo(expectedTeam);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTeamAsync(inputTeam), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
