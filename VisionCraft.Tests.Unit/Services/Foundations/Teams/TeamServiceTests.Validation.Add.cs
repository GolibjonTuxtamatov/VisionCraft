using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using VisionCraft.Models.Teams;
using VisionCraft.Models.Teams.Exceptions;
using Xunit;

namespace VisionCraft.Tests.Unit.Services.Foundations.Teams
{
    public partial class TeamServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfTeamIsNullAndLogItAsync()
        {
            //given
            Team nullTeam = null;

            var nullTeamException = new NullTeamException();

            var expectedTeamValidationException =
                new TeamValidationException(nullTeamException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertTeamAsync(nullTeam))
                    .ThrowsAsync(nullTeamException);

            //when
            ValueTask<Team> addTeamTask = this.teamService.AddTeamAsync(nullTeam);

            TeamValidationException actualTeamValidationException =
                await Assert.ThrowsAsync<TeamValidationException>(addTeamTask.AsTask);

            //then
            actualTeamValidationException.Should().BeEquivalentTo(expectedTeamValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedTeamValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTeamAsync(nullTeam), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
