using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Persistent;
using System.Configuration;
using UnitTest.TestModel;
using System.Collections.ObjectModel;
using Logging;
using System.IO;
using Common.Utility;

namespace UnitTest
{
    /// <summary>
    /// Summary description for PersistentTest
    /// </summary>
    [TestClass]
    public class PersistentTest
    {
        private const string createScript = "UnitTest.TestModel.Persistent.Create.sql";
        private const string foreignKeyScript = "UnitTest.TestModel.Persistent.ForeignKey.sql";
        private const string testDatabaseName = "TestModel";

        private StreamReader commandGenerateStream = null;

        static SqlServerORMapper orMapper = null;

        public PersistentTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            LoggerManager.Initialize("UnitTest.dll.config");            

            orMapper = new SqlServerORMapper(ConfigurationManager.ConnectionStrings[testDatabaseName].ConnectionString);

            if (orMapper.DatabaseExist(testDatabaseName))
            {
                orMapper.DropDatabase(testDatabaseName);
            }
            orMapper.CreateDatabase(testDatabaseName);

            using(var conn = orMapper.NewConnection())
            {
                conn.Open();
                orMapper.ExecuteSqlResourceFile(typeof(PersistentTest), createScript, conn, false);
                orMapper.ExecuteSqlResourceFile(typeof(PersistentTest), foreignKeyScript, conn, false);
            }
        }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        private void VerifyCommandGenerate(string cmd)
        {
            string result = commandGenerateStream.ReadLine();
            Assert.AreEqual(cmd, result.Substring(15));
            commandGenerateStream.ReadLine();
        }

        [TestMethod]
        public void CommandGenerateTest()
        {
            using (var conn = orMapper.NewConnection())
            {
                conn.Open();

                Database db = new Database
                {
                    Id = Guid.Empty,
                    Discriminator = TableType.Database,
                    Name = "db1",
                    Host = ".",
                    AuthenticationMode = AuthenticationMode.Windows,
                    Password = "111",
                    Port = "1433",
                    Provider = DatabaseProvider.SQLServer,
                    Status = ValidationState.Valid
                };

                commandGenerateStream = new StreamReader(AssemblyHelper.GetAssemblyEmbeddedResourceStream(this.GetType(), "UnitTest.CommandGenerate.txt"));

                orMapper.GetInsertCommands(db, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(db, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(db, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                Schema s1 = new Schema { Id = Guid.Empty, Name = "s1", OwnerId = db.Id };
                Schema s2 = new Schema { Id = Guid.Empty, Name = "s2", OwnerId = db.Id };

                orMapper.GetInsertCommands(s1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(s1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(s1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(s2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(s2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(s2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                var t1 = new Table { Id = Guid.Empty, Name = "t1", OwnerId = s1.Id };
                var t2 = new Table { Id = Guid.Empty, Name = "t2", OwnerId = s1.Id };
                var t3 = new Table { Id = Guid.Empty, Name = "t3", OwnerId = s2.Id };

                orMapper.GetInsertCommands(t1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(t1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(t1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(t2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(t2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(t2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(t3, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(t3, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(t3, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                Column c1 = new Column { Id = Guid.Empty, Name = "f1", Length = 5, ClassifierId = t1.Id, DataType = DataType.Int32 };
                Column c2 = new Column { Id = Guid.Empty, Name = "f2", Length = 15, ClassifierId = t1.Id, DataType = DataType.Int32 };

                orMapper.GetInsertCommands(c1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(c1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(c1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(c2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(c2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(c2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                UniqueKey key1 = new UniqueKey { Id = Guid.Empty, Name = "key1", OwnerId = t1.Id, IsPrimary = true };
                UniqueKeyFeature keyF1 = new UniqueKeyFeature { Id = Guid.Empty, UniqueKeyId = key1.Id, StructuralFeatureId = c1.Id };

                orMapper.GetInsertCommands(key1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(key1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(key1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(keyF1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(keyF1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(keyF1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                Column c3 = new Column { Id = Guid.Empty, Name = "f3", Length = 5, ClassifierId = t2.Id, DataType = DataType.Int32 };
                Column c4 = new Column { Id = Guid.Empty, Name = "f4", Length = 15, ClassifierId = t2.Id, DataType = DataType.Int32 };

                orMapper.GetInsertCommands(c3, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(c3, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(c3, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(c4, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(c4, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(c4, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                UniqueKey key2 = new UniqueKey { Id = Guid.Empty, Name = "key2", OwnerId = t2.Id, IsPrimary = true };
                UniqueKeyFeature keyF2 = new UniqueKeyFeature { Id = Guid.Empty, UniqueKeyId = key2.Id, StructuralFeatureId = c3.Id };
                KeyRelationship keyRel1 = new KeyRelationship { Id = Guid.Empty, UniqueKeyId = key1.Id, Name = "kr1", OwnerId = t2.Id };
                KeyRelationshipFeature krf1 = new KeyRelationshipFeature { Id = Guid.Empty, KeyRelationshipId = keyRel1.Id, StructuralFeatureId = c3.Id };

                orMapper.GetInsertCommands(key2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(key2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(key2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(keyF2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(keyF2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(keyF2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(keyRel1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(keyRel1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(keyRel1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(krf1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(krf1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(krf1, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                Column c5 = new Column { Id = Guid.Empty, Name = "f5", Length = 5, ClassifierId = t3.Id, DataType = DataType.Int32 };
                Column c6 = new Column { Id = Guid.Empty, Name = "f6", Length = 15, ClassifierId = t3.Id, DataType = DataType.Int32 };
                KeyRelationship keyRel2 = new KeyRelationship { Id = Guid.Empty, UniqueKeyId = key2.Id, Name = "kr2", OwnerId = t3.Id };
                KeyRelationshipFeature krf2 = new KeyRelationshipFeature { Id = Guid.Empty, KeyRelationshipId = keyRel2.Id, StructuralFeatureId = c5.Id };

                orMapper.GetInsertCommands(c5, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(c5, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(c5, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(c6, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(c6, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(c6, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(keyRel2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(keyRel2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(keyRel2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));

                orMapper.GetInsertCommands(krf2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetUpdateCommands(krf2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                orMapper.GetDeleteCommands(krf2, conn).ForEach(c => VerifyCommandGenerate(c.CommandText));
                
            }
        }

        [TestMethod]
        public void ManipulationTest()
        {
            Database db = new Database
            {
                Discriminator = TableType.Database,
                Name = "db1",
                Host = ".",
                AuthenticationMode = AuthenticationMode.Windows,
                Password = "111",
                Port = "1433",
                Provider = DatabaseProvider.SQLServer,
                Status = ValidationState.Valid
            };
            orMapper.Insert(db);

            Database loadedDb = new Database { Id = db.Id };
            orMapper.Load(loadedDb);
            Assert.AreEqual(loadedDb.PersistentState, PersistentState.Loaded);
            db.Name = "newdb1";
            orMapper.Update(db);
            orMapper.Load(loadedDb);
            Assert.AreEqual(db.Name, "newdb1");
            Assert.AreEqual(loadedDb.Name, "newdb1");

            Schema s1 = new Schema { Name = "s1", OwnerId = db.Id };
            Schema s2 = new Schema { Name = "s2", OwnerId = db.Id };
            orMapper.Insert(s1);
            orMapper.Insert(s2);

            Schema loadeds1 = new Schema { Id = s1.Id };
            orMapper.Load(loadeds1);
            Assert.AreEqual(loadeds1.PersistentState, PersistentState.Loaded);
            Schema loadeds2 = new Schema { Id = s2.Id };
            orMapper.Load(loadeds2);
            Assert.AreEqual(loadeds2.PersistentState, PersistentState.Loaded);

            var t1 = new Table { Name = "t1", OwnerId = s1.Id };
            var t2 = new Table { Name = "t2", OwnerId = s1.Id };
            var t3 = new Table { Name = "t3", OwnerId = s2.Id };
            orMapper.Insert(t1);
            orMapper.Insert(t2);
            orMapper.Insert(t3);

            Table loadedt1 = new Table { Id = t1.Id };
            orMapper.Load(loadedt1);
            Assert.AreEqual(loadedt1.PersistentState, PersistentState.Loaded);
            Table loadedt2 = new Table { Id = t2.Id };
            orMapper.Load(loadedt2);
            Assert.AreEqual(loadedt2.PersistentState, PersistentState.Loaded);
            Table loadedt3 = new Table { Id = t3.Id };
            orMapper.Load(loadedt3);
            Assert.AreEqual(loadedt3.PersistentState, PersistentState.Loaded);

            Column c1 = new Column { Name = "f1", Length = 5, ClassifierId = t1.Id, DataType = DataType.Int32 };
            Column c2 = new Column { Name = "f2", Length = 15, ClassifierId = t1.Id, DataType = DataType.Int32 };
            orMapper.Insert(c1);
            orMapper.Insert(c2);

            Column loadedc1 = new Column { Id = c1.Id };
            orMapper.Load(loadedc1);
            Assert.AreEqual(loadedc1.PersistentState, PersistentState.Loaded);
            Column loadedc2 = new Column { Id = c2.Id };
            orMapper.Load(loadedc2);
            Assert.AreEqual(loadedc2.PersistentState, PersistentState.Loaded);

            c1.Length = 10;
            orMapper.Update(c1);
            orMapper.Load(loadedc1);
            Assert.AreEqual(c1.Length, 10);
            Assert.AreEqual(loadedc1.Length, 10);

            UniqueKey key1 = new UniqueKey { Name = "key1", OwnerId = t1.Id, IsPrimary = true };
            UniqueKeyFeature keyF1 = new UniqueKeyFeature { UniqueKeyId = key1.Id, StructuralFeatureId = c1.Id };
            orMapper.Insert(key1);
            orMapper.Insert(keyF1);

            UniqueKey loadedkey1 = new UniqueKey { Id = key1.Id };
            orMapper.Load(loadedkey1);
            Assert.AreEqual(loadedkey1.PersistentState, PersistentState.Loaded);
            UniqueKeyFeature loadedkeyF1 = new UniqueKeyFeature { Id = keyF1.Id };
            orMapper.Load(loadedkeyF1);
            Assert.AreEqual(loadedkeyF1.PersistentState, PersistentState.Loaded);

            Column c3 = new Column { Name = "f3", Length = 5, ClassifierId = t2.Id, DataType = DataType.Int32 };
            Column c4 = new Column { Name = "f4", Length = 15, ClassifierId = t2.Id, DataType = DataType.Int32 };
            orMapper.Insert(c3);
            orMapper.Insert(c4);

            Column loadedc3 = new Column { Id = c3.Id };
            orMapper.Load(loadedc3);
            Assert.AreEqual(loadedc3.PersistentState, PersistentState.Loaded);
            Column loadedc4 = new Column { Id = c4.Id };
            orMapper.Load(loadedc4);
            Assert.AreEqual(loadedc4.PersistentState, PersistentState.Loaded);

            UniqueKey key2 = new UniqueKey { Name = "key2", OwnerId = t2.Id, IsPrimary = true };
            UniqueKeyFeature keyF2 = new UniqueKeyFeature { UniqueKeyId = key2.Id, StructuralFeatureId = c3.Id };
            KeyRelationship keyRel1 = new KeyRelationship { UniqueKeyId = key1.Id, Name = "kr1", OwnerId = t2.Id };
            KeyRelationshipFeature krf1 = new KeyRelationshipFeature { KeyRelationshipId = keyRel1.Id, StructuralFeatureId = c3.Id };
            orMapper.Insert(key2);
            orMapper.Insert(keyF2);
            orMapper.Insert(keyRel1);
            orMapper.Insert(krf1);

            UniqueKey loadedkey2 = new UniqueKey { Id = key2.Id };
            orMapper.Load(loadedkey2);
            Assert.AreEqual(loadedkey2.PersistentState, PersistentState.Loaded);
            UniqueKeyFeature loadedkeyF2 = new UniqueKeyFeature { Id = keyF2.Id };
            orMapper.Load(loadedkeyF2);
            Assert.AreEqual(loadedkeyF2.PersistentState, PersistentState.Loaded);
            KeyRelationship loadedkeyRel1 = new KeyRelationship { Id = keyRel1.Id };
            orMapper.Load(loadedkeyRel1);
            Assert.AreEqual(loadedkeyRel1.PersistentState, PersistentState.Loaded);
            KeyRelationshipFeature loadedkrf1 = new KeyRelationshipFeature { Id = krf1.Id };
            orMapper.Load(loadedkrf1);
            Assert.AreEqual(loadedkrf1.PersistentState, PersistentState.Loaded);

            Column c5 = new Column { Name = "f5", Length = 5, ClassifierId = t3.Id, DataType = DataType.Int32 };
            Column c6 = new Column { Name = "f6", Length = 15, ClassifierId = t3.Id, DataType = DataType.Int32 };
            KeyRelationship keyRel2 = new KeyRelationship { UniqueKeyId = key2.Id, Name = "kr2", OwnerId = t3.Id };
            KeyRelationshipFeature krf2 = new KeyRelationshipFeature { KeyRelationshipId = keyRel2.Id, StructuralFeatureId = c5.Id };
            orMapper.Insert(c5);
            orMapper.Insert(c6);
            orMapper.Insert(keyRel2);
            orMapper.Insert(krf2);

            Column loadedc5 = new Column { Id = c5.Id };
            orMapper.Load(loadedc5);
            Assert.AreEqual(loadedc5.PersistentState, PersistentState.Loaded);
            Column loadedc6 = new Column { Id = c6.Id };
            orMapper.Load(loadedc6);
            Assert.AreEqual(loadedc6.PersistentState, PersistentState.Loaded);
            KeyRelationship loadedkeyRel2 = new KeyRelationship { Id = keyRel2.Id };
            orMapper.Load(loadedkeyRel2);
            Assert.AreEqual(loadedkeyRel2.PersistentState, PersistentState.Loaded);
            KeyRelationshipFeature loadedkrf2 = new KeyRelationshipFeature { Id = krf2.Id };
            orMapper.Load(loadedkrf2);
            Assert.AreEqual(loadedkrf2.PersistentState, PersistentState.Loaded);
        }
    }
}
