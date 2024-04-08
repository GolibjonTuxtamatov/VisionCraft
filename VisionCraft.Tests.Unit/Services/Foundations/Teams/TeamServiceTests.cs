using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.Teams;
using VisionCraft.Services.Foundations.Teams;
using Xeptions;

namespace VisionCraft.Tests.Unit.Services.Foundations.Teams
{
    public partial class TeamServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ITeamService teamService;

        public TeamServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.teamService = new TeamService(
                storageBrokerMock.Object,
                loggingBrokerMock.Object);
        }

        private static Team CreateRandomTeam() =>
            CreateFiller().Create();

        private static Filler<Team> CreateFiller()
        {
            var filler = new Filler<Team>();

            string randomEmail = GetRandomString() + "@gmail.com";

            filler.Setup().OnProperty(team =>
                team.Email).Use(randomEmail);

            return filler;
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption exception) =>
            actualException => actualException.SameExceptionAs(exception);

        private SqlException CreateSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}
