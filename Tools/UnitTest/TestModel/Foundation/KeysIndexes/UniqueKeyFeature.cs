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
	public partial class UniqueKeyFeature : Element
	{
		public UniqueKeyFeature()
			: base(TableType.UniqueKeyFeature)
		{
		}

		#region ORMapping Columns
		
		private Guid uniqueKeyId;
		private Guid structuralFeatureId;
		private int? featureOrdinal;
        private bool isAscending;
        private string alias;

		[Column(Type = DbType.Guid)]
		public Guid UniqueKeyId
		{
			get { return uniqueKeyId; }
			set { uniqueKeyId = value;  }
		}

		[Column(Type = DbType.Guid)]
		public Guid StructuralFeatureId
		{
			get { return structuralFeatureId; }
			set { structuralFeatureId = value;  }
		}

		[Column(Type = DbType.Int32, AllowNull = true)]
		public int? FeatureOrdinal
		{
			get { return featureOrdinal; }
			set { featureOrdinal = value;  }
		}

		[Column(Type = DbType.Boolean, AllowNull = true)]
        public bool IsAscending
        {
            get { return isAscending; }
            set { isAscending = value;  }
        }

        [Column(Type = DbType.String, AllowNull = true)]
        public string Alias
        {
            get { return alias; }
            set { alias = value;  }
        }

		#endregion
		
	}
}
