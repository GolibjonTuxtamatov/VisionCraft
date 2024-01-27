using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using VisionCraft.Models.CVs.Exceptions;
using Xunit;

namespace VisionCraft.Tests.Unit.Services.Foundations.CVs
{
    public partial class CVServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllCVsIfSqlErrorOccursAndLogIt()
        {
            //given
            SqlException sqlException = GetSqlException();

            var faildStorageCVException = new FailedStorageCVException(sqlException);

            var expectedCVDependecyException =
                new CVDependencyException(faildStorageCVException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllCVs())
                .Throws(sqlException);

            //when
            Action retrieveAllCVsAction = () => this.cVService.RetrieveAllCVs();

            CVDependencyException actualCVdependencyException =
                Assert.Throws<CVDependencyException>(retrieveAllCVsAction);

            //then
            actualCVdependencyException.Should().BeEquivalentTo(expectedCVDependecyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllCVs(), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedCVDependecyException))),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
