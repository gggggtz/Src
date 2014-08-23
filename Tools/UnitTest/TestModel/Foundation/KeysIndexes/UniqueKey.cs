/// <copyright>
/// Copyright Unisys 2014.  All rights reserved.
/// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Xml.Serialization;
using Common.Persistent.ORMapping;
using Common.Extensions;
using Logging;

namespace UnitTest.TestModel
{
	[Table]
	[Serializable]
	public partial class UniqueKey : ModelElement
	{
		public UniqueKey()
			: base(TableType.UniqueKey)
		{
		}

		#region ORMapping Columns

		private DeferrabilityType deferrability = DeferrabilityType.NotDeferrable;
		private bool isPrimary;

		[Column(Type = DbType.Int16)]
		public DeferrabilityType Deferrability
		{
			get { return deferrability; }
			set { deferrability = value;  }
		}

		[Column(Type = DbType.Boolean)]
		public bool IsPrimary
		{
			get { return isPrimary; }
			set { isPrimary = value;  }
		}

		#endregion
		
	}
}
