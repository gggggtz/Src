/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>using System;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common.Persistent.ORMapping;

namespace Unitest.TestModel
{
	[Table]
	[Serializable]
	public class Dependency : Element
	{
		public Dependency()
			: base(TableType.Dependency)
		{
		}

		#region ORMapping Columns

		private Guid? supplierId;
		private Guid? clientId;
		private string kind;

		[Column(Type = DbType.Guid, AllowNull = true)]
		public Guid? SupplierId
		{
			get { return supplierId; }
			set { supplierId = value;  }
		}

		[Column(Type = DbType.Guid, AllowNull = true)]
		public Guid? ClientId
		{
			get { return clientId; }
			set { clientId = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string Kind
		{
			get { return kind; }
			set { kind = value;  }
		}
		#endregion
	}
}
