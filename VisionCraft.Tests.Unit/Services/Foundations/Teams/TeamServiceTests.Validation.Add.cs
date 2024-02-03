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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfTeamIsInvalidAndLogItAsync(string invalidText)
        {
            //given
            Team invalidTeam = new Team
            {
                Name = invalidText,
            };

            var invalidTeamException = new InvalidTeamException();

            invalidTeamException.AddData(
                key: nameof(Team.Id),
                values: "Id is required");

            invalidTeamException.AddData(
                key: nameof(Team.Name),
                values: "Text is required");

            invalidTeamException.AddData(
                key: nameof(Team.Email),
                values: "Text is required");

            invalidTeamException.AddData(
                key: nameof(Team.Password),
                values: "Text is required");

            var expectedTeamValidationeException =
                new TeamValidationException(invalidTeamException);
            //when
            ValueTask<Team> addTeamTask = this.teamService.AddTeamAsync(invalidTeam);

            TeamValidationException actualTeamValidationException =
                await Assert.ThrowsAsync<TeamValidationException>(addTeamTask.AsTask);

            //then
            actualTeamValidationException.Should().BeEquivalentTo(expectedTeamValidationeException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedTeamValidationeException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTeamAsync(It.IsAny<Team>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfEmailIsInvalidAndLogItAsync()
        {
            //given
            Team invaidTeam = CreateRandomTeam();

            var invalidTeamException = new InvalidTeamException();

            invalidTeamException.AddData(
                key: nameof(Team.Email),
                values: "Email is invalid.");

            var expectedTeamValidationException =
                new TeamValidationException(invalidTeamException);

            //when
            ValueTask<Team> addTeamTask = this.teamService.AddTeamAsync(invaidTeam);

            TeamValidationException actualTeamValidationException =
                await Assert.ThrowsAsync<TeamValidationException>(addTeamTask.AsTask);

            //then
            actualTeamValidationException.Should().BeEquivalentTo(expectedTeamValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedTeamValidationException))), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
