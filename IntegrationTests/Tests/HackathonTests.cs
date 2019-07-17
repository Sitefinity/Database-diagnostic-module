using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBDiagnostics.Database;
using MbUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [Description("Some simple tests.")]
    public class HackathonTests
    {
        [Test]
        [Category(TestCategories.Hackathon)]
        [Author("Telerik Developer")]
        public void AssertDBDiagnosticsSerivceReturnsDatabaseTables()
        {
            var dbDiagnosticsService = new DatabaseDiagnosticsService();
            var dbTables = Task.Run(async () => await dbDiagnosticsService.GetDatabaseTables()).Result;
            Assert.IsNotNull(dbTables);
            Assert.IsTrue(dbTables.Count > 0);
        }

        [Test]
        [Category(TestCategories.Hackathon)]
        [Author("Telerik Developer")]
        public void AssertDBDiagnosticsSerivceReturnsDBSize()
        {
            var dbDiagnosticsService = new DatabaseDiagnosticsService();
            var dbSize = Task.Run(async () => await dbDiagnosticsService.GetDBSize()).Result;
            Assert.IsNotNull(dbSize);
            Assert.IsNotNull(dbSize.Name);
            Assert.IsNotNull(dbSize.Size);
        }

        [Test]
        [Category(TestCategories.Hackathon)]
        [Author("Telerik Developer")]
        [Description("A simple test that will pass.")]
        public void PassingTest()
        {
            int expectedValue = 2;

            int actualValue = 1 + 1;

            Assert.AreEqual<int>(expectedValue, actualValue);
        }

        [Test]
        [Category(TestCategories.Hackathon)]
        [Author("Telerik Developer")]
        [Description("A simple test that will fail.")]
        public void FailingTest()
        {
            int expectedValue = 2;

            int actualValue = 1 + 1 + 1;

            Assert.AreEqual<int>(expectedValue, actualValue);
        }

        [Test]
        [Category(TestCategories.Hackathon)]
        [Author("Telerik Developer")]
        [Description("A simple test that will be ignored. You can run it manually in the Test runner.")]
        public void IgnoredTest()
        {
            int expectedValue = 2;

            int actualValue = 1 + 1;

            Assert.AreEqual<int>(expectedValue, actualValue);
        }
    }
}
