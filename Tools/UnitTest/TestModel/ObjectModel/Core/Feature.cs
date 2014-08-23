/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Serialization;
using Common;
using Logging;
using Common.Persistent.ORMapping;

namespace UnitTest.TestModel
{
	[Table]
	[Serializable]
	public partial class Feature : ModelElement
	{
		public Feature()
			: this(TableType.Feature)
		{
		}

		public Feature(TableType type)
			: base(type)
		{
			changeKind = ChangableKind.Changeable;
		}

		#region ORMapping Columns

		private int length;
		/// <summary>
		/// The precision for LINKCOUNT is acutally the vlue of the LINKCOUNT.
		/// For example COUNTOFDS1 COUNT(33); The precision is 33.
		/// </summary>
		private int precision;
		private int scale;
		private int ordinal;
		private DataType dataType;
		private ChangableKind changeKind;
		private OrderKind orderKind;
		private ScopeKind scopeKind;
		private Guid? classifierId;
		private Guid? initialValueId;

		[Column(Type = DbType.Int32, AllowNull = true)]
		public int Length
		{
			get { return length; }
			set { length = value;  }
		}

		[Column(Type = DbType.Int32, AllowNull = true)]
		public int Precision
		{
			get { return precision; }
			set { precision = value;  }
		}

		[Column(Type = DbType.Int32, AllowNull = true)]
		public int Scale
		{
			get { return scale; }
			set { scale = value;  }
		}

		[Column(Type = DbType.Int32, AllowNull = true)]
		public int Ordinal
		{
			get { return ordinal; }
			set { ordinal = value;  }
		}

		[Column(Type = DbType.Int16)]
		public DataType DataType
		{
			get { return dataType; }
			set { dataType = value;  }
		}

		[Column(Type = DbType.Int16)]
		public ChangableKind ChangeKind
		{
			get { return changeKind; }
			set { changeKind = value;  }
		}

		[Column(Type = DbType.Int16)]
		public OrderKind OrderKind
		{
			get { return orderKind; }
			set { orderKind = value;  }
		}

		[Column(Type = DbType.Int16)]
		public ScopeKind ScopeKind
		{
			get { return scopeKind; }
			set { scopeKind = value;  }
		}

		[Column(Type = DbType.Guid, AllowNull = true)]
		public Guid? ClassifierId
		{
			get { return classifierId; }
			set { classifierId = value;  }
		}

		[XmlIgnore]
		[Column(Type = DbType.Guid, AllowNull = true)]
		public Guid? InitialValueId
		{
			get { return initialValueId; }
			set { initialValueId = value;  }
		}

		#endregion

	}
}

