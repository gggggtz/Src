/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Common.Persistent.ORMapping;
using Common.Extensions;
using Logging;

namespace UnitTest.TestModel
{
	[Table]
	[Serializable]
	public partial class Table : Classifier
	{
		public Table()
			: base(TableType.Table)
		{
		}

		#region ORMapping Columns

		private bool isTemporary;
		private string temporaryScope;
		private bool isSystem;

		[Column(Type = DbType.Boolean)]
		public bool IsTemporary
		{
			get { return isTemporary; }
			set { isTemporary = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string TemporaryScope
		{
			get { return temporaryScope; }
			set { temporaryScope = value;  }
		}

		[Column(Type = DbType.Boolean)]
		public bool IsSystem
		{
			get { return isSystem; }
			set { isSystem = value;  }
		}

		#endregion
	}
}
