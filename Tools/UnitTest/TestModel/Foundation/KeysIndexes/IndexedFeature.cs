/// <copyright>
/// Copyright Unisys 2014.  All rights reserved.
/// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Logging;
using Common.Persistent.ORMapping;

namespace UnitTest.TestModel
{
	[Table]
	[Serializable]
	public partial class IndexedFeature : Element
	{
		public IndexedFeature()
			: base(TableType.IndexedFeature)
		{
		}

		#region ORMapping Columns

		private bool isAscending;
		private int? indexOrdinal;
		private Guid indexId;
		private Guid structuralFeatureId;

		[Column(Type = DbType.Boolean)]
		public bool IsAscending
		{
			get { return isAscending; }
			set { isAscending = value;  }
		}

		[Column(Type = DbType.Int16, AllowNull = true)]
		public int? IndexOrdinal
		{
			get { return indexOrdinal; }
			set { indexOrdinal = value;  }
		}

		[Column(Type = DbType.Guid)]
		public Guid IndexId
		{
			get { return indexId; }
			set { indexId = value;  }
		}

		[Column(Type = DbType.Guid)]
		public Guid StructuralFeatureId
		{
			get { return structuralFeatureId; }
			set { structuralFeatureId = value;  }
		}

		#endregion
		
	}
}
