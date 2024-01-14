using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
    }
}
