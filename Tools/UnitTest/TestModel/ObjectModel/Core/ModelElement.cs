/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>using System;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Xml.Serialization;
using Logging;
using Common.Persistent.ORMapping;

namespace Unitest.TestModel
{
	[Table]
	[Serializable]
	public partial class ModelElement : Element
	{
		private bool membersChanged = false;

		public ModelElement()
			: this(TableType.ModelElement)
		{
		}

		public ModelElement(TableType type)
			: base(type)
		{
			visibility = VisibilityKind.Public;
		}

		#region ORMapping Columns

		private Guid? ownerId;
		private string name;
		private VisibilityKind visibility;
		private Int64 memberRevision;
		private string schemaVersion;
        private string alias;

		[Column(Type = DbType.Guid, AllowNull = true)]
		public Guid? OwnerId
		{
			get { return ownerId; }
			set { ownerId = value;  }
		}

		[Column(Type = DbType.String)]
		public string Name
		{
			get { return name; }
			set { name = value;  }
		}

		[Column(Type = DbType.Int16)]
		public VisibilityKind Visibility
		{
			get { return visibility; }
			set { visibility = value;  }
		}

		[XmlIgnore]
		[Column(Type = DbType.Int64)]
		public Int64 MemberRevision 
		{
			get { return memberRevision; }
			set { memberRevision = value; }
		}

		[Column(Type = DbType.String,AllowNull=true)]
		public string SchemaVersion
		{
			get { return schemaVersion; }
			set { schemaVersion = value;  }
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
