using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.CVs;
using VisionCraft.Services.Foundations.CVs;
using Xeptions;

namespace VisionCraft.Tests.Unit.Services.Foundations.CVs
{
    public partial class CVServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICVService cVService;

        public CVServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.cVService = new CVService(
                storageBroker: storageBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }

        private string GetRandomString() =>
            new MnemonicString().GetValue();

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption exception) =>
            actualException => actualException.SameExceptionAs(exception);

        private static CV CreateRandomCV() =>
            CreateFiller().Create();

        private static int GetRandomNumber() =>
            new Random().Next(2, 10);

        private static IQueryable<CV> CreateRandomCVs() =>
            CreateFiller().Create(GetRandomNumber()).AsQueryable();

        private static Filler<CV> CreateFiller() =>
            new Filler<CV>();
    }
}
