using FluentAssertions;
using Moq;
using VisionCraft.Models.CVs;
using VisionCraft.Models.CVs.Exceptions;
using Xunit;

namespace VisionCraft.Tests.Unit.Services.Foundations.CVs
{
    public partial class CVServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCVIsNullAndLogItAsync()
        {
            //given
            CV nullCV = null;

            var nullCVException = new NullCVException();

            var expectedCVValidationException =
                new CVValidationException(nullCVException);

            //when
            ValueTask<CV> addCVTask = this.cVService.AddCVAsync(nullCV);

            CVValidationException actualCVValidationException =
                await Assert.ThrowsAsync<CVValidationException>(addCVTask.AsTask);

            //then
            actualCVValidationException.Should().BeEquivalentTo(expectedCVValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedCVValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCVAsync(It.IsAny<CV>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCVIsInvalidAndLogItAsync()
        {
            //given
            var invalidCV = new CV();

            var invalidCVException = new InvalidCVException();

            invalidCVException.AddData(
                key: nameof(CV.Id),
                values: "Id is required");

            var expectedCVValidationException =
                new CVValidationException(invalidCVException);

            //when
            ValueTask<CV> addCVTask = this.cVService.AddCVAsync(invalidCV);

            CVValidationException actualCVValidationException =
                await Assert.ThrowsAsync<CVValidationException>(addCVTask.AsTask);

            //then
            actualCVValidationException.Should().BeEquivalentTo(expectedCVValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedCVValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCVAsync(It.IsAny<CV>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
