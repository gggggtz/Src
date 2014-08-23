/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Common;
using Logging;
using Common.Persistent.ORMapping;

namespace UnitTest.TestModel
{
	/// <summary>
	/// The empty base class for all databases
	/// </summary>
	[Table]
	[Serializable]
	public class Database : NameSpace
	{
		private const string SqlServerDataTypeNamePrefix = "sql_";
		public Database()
			: base(TableType.Database)
		{
		}

        public Database(TableType type)
            :base(type)
        {
        }

		#region ORMapping Columns

		private string host;
		private DatabaseProvider provider;
		private AuthenticationMode authenticationMode;
		private string loginUser;
		private string password;
		private bool rememberPassword;
		private string port;
		private string fullName;
		private string defaultCharacterSetName;
		private string defaultCollationName;
		private int? ccsVersion;
		private DBCcsVersion dbCcsVersion = DBCcsVersion.NONE;
		private AliasCcsVersion aliasCcsVersion = AliasCcsVersion.NONE;
		private byte[] initValues;
		private byte[] nullValues;
        private string minorVersion;
        private DateTime? extractedUTC = null;
        private string appGroup;
		private string hostId;

		[Column(Type = DbType.String, AllowNull = true)]
		public string Host
		{
			get { return host; }
			set { host = value;  }
		}
		[Column(Type = DbType.Int16)]
		public DatabaseProvider Provider
		{
			get { return provider; }
			set { provider = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string Port
		{
			get { return port; }
			set { port = value;  }
		}

		[Column(Type = DbType.Int16)]
		public AuthenticationMode AuthenticationMode
		{
			get { return authenticationMode; }
			set { authenticationMode = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string LoginUser
		{
			get { return loginUser; }
			set { loginUser = value;  }
		}

        [Column(Type = DbType.String, AllowNull = true)]
		public string Password
		{
			get { return password; }
			set { password = value;  }
		}

		[Column(Type = DbType.Boolean, AllowNull = false)]
		public bool RememberPassword
		{
			get { return rememberPassword; }
			set { rememberPassword = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string DefaultCharacterSetName
		{
			get { return defaultCharacterSetName; }
			set { defaultCharacterSetName = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string DefaultCollationName
		{
			get { return defaultCollationName; }
			set { defaultCollationName = value;  }
		}

		[Column(Type = DbType.Int16, AllowNull = true)]
		public int? CcsVersion
		{
			get { return ccsVersion; }
			set { ccsVersion = value;  }
		}

		[Column(Type = DbType.Int16)]
		public DBCcsVersion DBCcsVersion
		{
			get { return dbCcsVersion; }
			set 
			{
				if (dbCcsVersion != value)
				{
					dbCcsVersion = value;
					
				}
			}
		}

		[Column(Type = DbType.Int16, AllowNull = true)]
		public AliasCcsVersion AliasCcsVersion
		{
			get { return aliasCcsVersion; }
			set { aliasCcsVersion = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string FullName
		{
			get { return fullName; }
			set { fullName = value;  }
		}

		[Column(Type = DbType.Binary, AllowNull = true)]
		public byte[] InitValues
		{
			get { return initValues; }
			set { initValues = value;  }
		}

		[Column(Type = DbType.Binary, AllowNull = true)]
		public byte[] NullValues
		{
			get { return nullValues; }
			set { nullValues = value;  }
		}

        [Column(Type = DbType.String, AllowNull= true)]
        public string MinorVersion
        {
            get { return minorVersion; }
            set { minorVersion = value;  }
        }

        [Column(Type = DbType.DateTime, AllowNull = true)]
        public DateTime? ExtractedUTC
        {
			get { return extractedUTC; }
			set { extractedUTC = value;  }
        }

        [Column(Type = DbType.String, AllowNull = true)]
        public string AppGroup
        {
            get { return appGroup; }
            set { appGroup = value;  }
        }

		[Column(Type = DbType.String, AllowNull = true)]
		public string HostId
		{
			get { return hostId; }
			set { hostId = value;  }
		}

		#endregion
	}
}
