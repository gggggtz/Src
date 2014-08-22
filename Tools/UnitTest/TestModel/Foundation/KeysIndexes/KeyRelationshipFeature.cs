/// <copyright>
/// Copyright Unisys 2014.  All rights reserved.
/// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Serialization;
using Logging;
using Common.Persistent.ORMapping;

namespace Unitest.TestModel
{
	[Table]
	[Serializable]
	public partial class KeyRelationshipFeature : Element
	{
		public KeyRelationshipFeature():base(TableType.KeyRelationshipFeature)
		{
		}

		#region ORMapping Columns
		private int? featureOrdinal;
		private Guid keyRelationshipId;
		private Guid structuralFeatureId;

		[Column(Type = DbType.Int32, AllowNull = true)]
		public int? FeatureOrdinal
		{
			get { return featureOrdinal; }
			set { featureOrdinal = value;  }
		}

		[Column(Type = DbType.Guid)]
		public Guid KeyRelationshipId
		{
			get { return keyRelationshipId; }
			set { keyRelationshipId = value;  }
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
