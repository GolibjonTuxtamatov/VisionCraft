using EFxceptions.Models.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using VisionCraft.Models.Teams;
using VisionCraft.Models.Teams.Exceptions;
using Xunit;

namespace VisionCraft.Tests.Unit.Services.Foundations.Teams
{
    public partial class TeamServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Team someTeam = CreateRandomTeam();
            SqlException sqlException = CreateSqlException();
            var failedTeamStorageException = new FailedTeamStorageException(sqlException);

            var expectedTeamDependencyException =
                new TeamDependencyException(failedTeamStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertTeamAsync(someTeam))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Team> addTeamTask = this.teamService.AddTeamAsync(someTeam);

            TeamDependencyException actualTeamDependencyException =
                await Assert.ThrowsAsync<TeamDependencyException>(addTeamTask.AsTask);

            // then
            actualTeamDependencyException.Should().BeEquivalentTo(expectedTeamDependencyException);


            this.storageBrokerMock.Verify(broker =>
                broker.InsertTeamAsync(someTeam), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedTeamDependencyException))), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldTrowDependencyValidationExceptionOnAddIfDuplicateErrorOccursAndLogItAsync()
        {
            // given
            Team someTeam = CreateRandomTeam();
            string someMessage = GetRandomString();
            var duplicateKeyException = new DuplicateKeyException(someMessage);

            var alreadyExistsTeamException =
                new AlreadyExistsTeamException(duplicateKeyException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertTeamAsync(someTeam))
                    .ThrowsAsync(duplicateKeyException);

            var expectedTeamDependencyValidationException =
                new TeamDependencyValidationException(alreadyExistsTeamException);

            // when
            ValueTask<Team> addTeamTask = this.teamService.AddTeamAsync(someTeam);

            TeamDependencyValidationException actualTeamDependencyValidationException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(addTeamTask.AsTask);

            // then
            actualTeamDependencyValidationException.Should().BeEquivalentTo(
                expectedTeamDependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTeamAsync(someTeam), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogError(It.Is(SameExceptionAs(
                expectedTeamDependencyValidationException))), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Team someTeam = CreateRandomTeam();
            var serviceException = new Exception();

            var failedTeamServiceException =
                new FailedTeamServiceException(serviceException);

            var expectedTeamServiceException =
                new TeamServiceException(failedTeamServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertTeamAsync(someTeam))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Team> addTeamTask =
                this.teamService.AddTeamAsync(someTeam);

            TeamServiceException actualTeamServiceException =
                await Assert.ThrowsAsync<TeamServiceException>(addTeamTask.AsTask);

            // then
            actualTeamServiceException.Should().BeEquivalentTo(expectedTeamServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTeamAsync(someTeam), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTeamServiceException))), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
