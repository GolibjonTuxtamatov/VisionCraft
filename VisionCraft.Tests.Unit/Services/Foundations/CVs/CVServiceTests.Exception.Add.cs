using EFxceptions.Models.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using VisionCraft.Models.CVs;
using VisionCraft.Models.CVs.Exceptions;
using Xunit;
using Xunit.Abstractions;

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

        [Fact]
        public async Task ShouldThrowDependecyExceptionOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            //given
            CV someCV = CreateRandomCV();
            var duplicateKeyException = new DuplicateKeyException(GetRandomString());
            var alreadExistCvException = new AlreadyExistCVException(duplicateKeyException);

            var expectedCVDependecyValidationException =
                new CVDependencyValidationException(alreadExistCvException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertCVAsync(someCV))
                    .ThrowsAsync(duplicateKeyException);

            //then
            ValueTask<CV> addCVTask = this.cVService.AddCVAsync(someCV);

            CVDependencyValidationException actualCVDependencyValidationException =
                await Assert.ThrowsAsync<CVDependencyValidationException>(addCVTask.AsTask);

            //then
            actualCVDependencyValidationException.Should().BeEquivalentTo(expectedCVDependecyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCVAsync(someCV), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedCVDependecyValidationException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given
            CV someCV = CreateRandomCV();

            var exception = new Exception(GetRandomString());

            var failedServiceException = new FailedServiceException(exception);

            var expectedCVServiceException = new CVServiceException(failedServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertCVAsync(someCV))
                .ThrowsAsync(exception);

            //when
            ValueTask<CV> addCVTask =
                this.cVService.AddCVAsync(someCV);

            CVServiceException actualCVServiceException =
                await Assert.ThrowsAsync<CVServiceException>(addCVTask.AsTask);

            //then
            actualCVServiceException.Should().BeEquivalentTo(expectedCVServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCVAsync(someCV),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedCVServiceException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
