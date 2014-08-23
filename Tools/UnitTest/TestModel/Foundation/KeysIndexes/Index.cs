/// <copyright>
/// Copyright Unisys 2014.  All rights reserved.
/// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Serialization;
using Logging;
using Common.Persistent.ORMapping;

namespace UnitTest.TestModel
{
	[Table]
	[Serializable]
	public partial class Index : ModelElement
	{
		public Index()
			: this(TableType.Index)
		{
		}

		public Index(TableType type)
			: base(type)
		{
		}

		#region ORMapping Columns

		private bool isPartitioning;
		private bool isSorted;
		private bool isUnique;
		private string filterCondition;
		private bool isNullable;
		private bool autoUpdate;

		[Column(Type = DbType.Boolean)]
		public bool IsPartitioning
		{
			get { return isPartitioning; }
			set { isPartitioning = value;  }
		}

		[Column(Type = DbType.Boolean)]
		public bool IsSorted
		{
			get { return isSorted; }
			set { isSorted = value;  }
		}

		[Column(Type = DbType.Boolean)]
		public bool IsUnique
		{
			get { return isUnique; }
			set { isUnique = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string FilterCondition
		{
			get { return filterCondition; }
			set { filterCondition = value;  }
		}

		[Column(Type = DbType.Boolean)]
		public bool IsNullable
		{
			get { return isNullable; }
			set { isNullable = value;  }
		}

		[Column(Type = DbType.Boolean)]
		public bool AutoUpdate
		{
			get { return autoUpdate; }
			set { autoUpdate = value;  }
		}

		#endregion

	}
}
