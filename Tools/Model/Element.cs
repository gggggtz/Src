using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    public class Element : Entity
    {

        #region Static Constructor

        static Element()
        {
            //OrMapper = EntityAccesserFactory.GetAccesser(EntityAccesserType.SqlServer, new string[] { Common.Cloud.CloudUtility.AzureSQLServerDatabaseConnectionString, "Id", "dbo" }) as IBatchORMapper;
            //var dbName = OrMapper.GetDatabaseName();
            //if (!OrMapper.DatabaseExist(dbName))
            //{
            //    OrMapper.CreateDatabase(dbName);
            //    using (var conn = OrMapper.NewConnection())
            //    {
            //        conn.Open();
            //        OrMapper.ExecuteSqlResourceFile(typeof(Element), "Model.Create.sql", conn, false);
            //    }
            //}
        }

        #endregion

        #region ORMapping Columns

        public System.Guid Id { get; set; }

        public Discriminator Discriminator { get; set; }

        public Guid? ReservedBy { get; set; }

        public Int64 Revision { get; set; }

        public DateTime LastModified { get; set; }

        public DateTime Created { get; set; }

        public ElementState State { get; set; }

        #endregion

        public Element()
            : this(Discriminator.Element)
        {

        }

        public Element(Discriminator discriminator)
        {
            Discriminator = discriminator;
            Id = Guid.NewGuid();
        }
    }
}
