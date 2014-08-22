/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Persistent.ORMapping;
using Common;
using Logging;

namespace Unitest.TestModel
{
	[Table]
	[Serializable]
	public partial class Column : Feature
	{
		private static readonly int maxLen = 2 * 1024 * 1024;

		public Column()
			: base(TableType.Column)
		{
		}

		#region ORMapping Columns

		private bool isNullable;
		private string collationName;
		private string characterSetName;

		[Column(Type = DbType.Boolean)]
		public bool IsNullable
		{
			get { return isNullable; }
			set { isNullable = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string CollationName
		{
			get { return collationName; }
			set { collationName = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string CharacterSetName
		{
			get { return characterSetName; }
			set { characterSetName = value;  }
		}

		#endregion

	}
}
