using FluentAssertions;
using Force.DeepCloner;
using Moq;
using VisionCraft.Models.CVs;
using Xunit;

namespace VisionCraft.Tests.Unit.Services.Foundations.CVs
{
    public partial class CVServiceTests
    {
        [Fact]
        public async Task ShouldAddCVAsync()
        {
            //given
            CV randomCV = CreateRandomCV();
            CV inputCV = randomCV;
            CV storageCV = inputCV;
            CV expectedCV = storageCV.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertCVAsync(inputCV))
                .ReturnsAsync(storageCV);

            //when
            CV actualCV = await this.cVService.AddCVAsync(inputCV);

            //then
            actualCV.Should().BeEquivalentTo(expectedCV);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCVAsync(inputCV), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
