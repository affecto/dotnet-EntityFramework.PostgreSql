using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.EntityFramework.PostgreSql.Tests
{
    [TestClass]
    public class PostgreSqlConfigurationTests
    {
        private PostgreSqlConfiguration sut;

        [TestInitialize]
        public void Setup()
        {
            sut = PostgreSqlConfiguration.Settings;
        }

        [TestMethod]
        public void SchemaCollectionIsNotNull()
        {
            Assert.IsNotNull(sut.Schemas);
        }

        [TestMethod]
        public void SchemasAreReadFromConfigFile()
        {
            Assert.AreEqual("SomeSchemaName", sut.Schemas["TestSchema"]);
            Assert.AreEqual("YetSomeSchemaName", sut.Schemas["AnotherTestSchema"]);
        }
    }
}