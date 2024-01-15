using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using VisionCraft.Models.CVs;
using VisionCraft.Models.CVs.Exceptions;
using Xunit;

namespace VisionCraft.Tests.Unit.Services.Foundations.CVs
{
    public partial class CVServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependecyCriticalExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            CV someCV = CreateRandomCV();
            SqlException sqlException = GetSqlException();
            var failedStorageCVException = new FailedStorageCVException(sqlException);

            var expectedCVDependencyException =
                new CVDependencyException(failedStorageCVException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertCVAsync(someCV))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<CV> addCVTask = this.cVService.AddCVAsync(someCV);

            CVDependencyException actualCVDependencyException =
                await Assert.ThrowsAsync<CVDependencyException>(addCVTask.AsTask);

            //then
            actualCVDependencyException.Should().BeEquivalentTo(expectedCVDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCVAsync(someCV), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedCVDependencyException)))
                    ,Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
