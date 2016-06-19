﻿using System;
using System.Data;
using System.Data.Hsql;
using System.Linq;
using NUnit.Framework;

namespace SharpHSQL.IntegrationTests.ProviderTests {
    [TestFixture]
    class BuiltItFunctionsTests : BaseQueryTest {
        [Test]
        public void FunctionMax_ShouldReturnMaximalValue() {
            var dbPrototype = new DataSet("mytest");
            var clientsTable = GenerateTableClients();
            dbPrototype.Tables.Add(clientsTable);

            TestQuery(dbPrototype, connection => {
                var commandText = "SELECT MAX(\"clients\".\"id\") FROM \"clients\";";
                var command = new SharpHsqlCommand(commandText, connection);
                var result = command.ExecuteScalar();
                Assert.NotNull(result);
                Assert.AreEqual(Enumerable.Range(1, 10).Max(), (Int32)result);
            });
        }

        [Test]
        public void FunctionSum_ShouldReturnSumValues() {
            var dbPrototype = new DataSet("mytest");
            var clientsTable = GenerateTableClients();
            dbPrototype.Tables.Add(clientsTable);

            TestQuery(dbPrototype, connection => {
                var commandText = "SELECT SUM(\"clients\".\"id\") FROM \"clients\";";
                var cmd = new SharpHsqlCommand(commandText, connection);
                var result = cmd.ExecuteScalar();
                Assert.NotNull(result);
                Assert.AreEqual(Enumerable.Range(1, 10).Sum(), (Int32)result);
            });
        }

        [Test]
        public void FunctionCountt_ShouldReturnCountValues() {
            var dbPrototype = new DataSet("mytest");
            var clientsTable = GenerateTableClients();
            dbPrototype.Tables.Add(clientsTable);

            TestQuery(dbPrototype, connection => {
                var cmd = new SharpHsqlCommand("", connection);
                cmd.CommandText = "SELECT COUNT(\"clients\".\"id\") FROM \"clients\";";
                var result = cmd.ExecuteScalar();
                Assert.NotNull(result);
                Assert.AreEqual(Enumerable.Range(1, 10).Count(), (Int32)result);
            });
        }

        [Test]
        public void FunctionAvg_ShouldReturnAverageValue() {
            var dbPrototype = new DataSet("mytest");
            var clientsTable = GenerateTableClients();
            dbPrototype.Tables.Add(clientsTable);

            TestQuery(dbPrototype, connection => {
                var cmd = new SharpHsqlCommand("", connection);
                cmd.CommandText = "SELECT AVG(\"clients\".\"id\") FROM \"clients\";";
                var result = (Double)cmd.ExecuteScalar();
                Assert.NotNull(result);
                Assert.AreEqual(Enumerable.Range(1, 10).Average(), result);
            });
        }

        [Test]
        [Ignore("Not correct sql")]
        public void FunctionAbs_ShouldReturnAbsoluteValue() {
            var dbPrototype = new DataSet("mytest");
            var clientsTable = GenerateTableClients();
            dbPrototype.Tables.Add(clientsTable);

            TestQuery(dbPrototype, connection => {
                var cmd = new SharpHsqlCommand("", connection);
                cmd.CommandText = "CALL ABS(-33.5632);";
                var result = cmd.ExecuteScalar();
                Assert.NotNull(result);
                Assert.AreEqual(33.5632, (Int32)result);
            });
        }

        [Test]
        public void FunctionUser_ShouldReturnUserName() {
            var dbPrototype = new DataSet("mytest");
            var clientsTable = GenerateTableClients();
            dbPrototype.Tables.Add(clientsTable);

            TestQuery(dbPrototype, connection => {
                var cmd = new SharpHsqlCommand("", connection);
                cmd.CommandText = "CALL USER();";
                var user = (String)cmd.ExecuteScalar();
                Assert.AreEqual("SA", user); // TODO: Why upper register?
            });
        }

        [Test]
        public void FunctionSqrt_ShouldReturnSquareRoot() {
            var dbPrototype = new DataSet("mytest");
            var clientsTable = GenerateTableClients();
            dbPrototype.Tables.Add(clientsTable);

            TestQuery(dbPrototype, connection => {
                var cmd = new SharpHsqlCommand("", connection);
                cmd.CommandText = "CALL SQRT(4);";
                var sqrt = (Double)cmd.ExecuteScalar();
                Assert.AreEqual(2, sqrt);
            });
        }

        [Test]
        public void FunctionSubstring_ShouldReturnSubstring() {
            var dbPrototype = new DataSet("mytest");
            var clientsTable = GenerateTableClients();
            dbPrototype.Tables.Add(clientsTable);

            TestQuery(dbPrototype, connection => {
                var cmd = new SharpHsqlCommand("", connection);
                cmd.CommandText = "CALL SUBSTRING('0123456', 3, 2);";
                var subs = (String)cmd.ExecuteScalar();
                Assert.AreEqual("23", subs);
            });
        }

        [Test]
        public void FunctionAscii_ShouldReturnCharCode() {
            var dbPrototype = new DataSet("mytest");
            var clientsTable = GenerateTableClients();
            dbPrototype.Tables.Add(clientsTable);

            TestQuery(dbPrototype, connection => {
                var cmd = new SharpHsqlCommand("", connection);
                cmd.CommandText = "CALL ASCII('A');";
                var ascii = (Int32)cmd.ExecuteScalar();
                Assert.AreEqual(65, ascii);
            });
        }
    }
}
