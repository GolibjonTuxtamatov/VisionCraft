using FluentAssertions;
using Moq;
using VisionCraft.Models.CVs;
using Xunit;

namespace VisionCraft.Tests.Unit.Services.Foundations.CVs
{
    public partial class CVServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllCvs()
        {
            //given
            IQueryable<CV> storageCVs = CreateRandomCVs();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllCVs())
                    .Returns(storageCVs);

            //when
            IQueryable<CV> actualCVs = this.cVService.RetrieveAllCVs();

            //then
            actualCVs.Should().BeEquivalentTo(storageCVs);

            this.storageBrokerMock.Verify(broker => broker.SelectAllCVs(),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
