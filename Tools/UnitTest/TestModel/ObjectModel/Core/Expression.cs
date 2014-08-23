/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Common.Persistent.ORMapping;
using System.Linq;
using System.Collections.ObjectModel;
using Common;
using Logging;

namespace UnitTest.TestModel
{
	[Table]
	[Serializable]
	public partial class Expression : Element
	{
		public Expression()
			: base(TableType.Expression)
		{
			this.Language = UnitTest.TestModel.Language.CSharp;
		}

		#region ORMapping Columns

		private string body;
		private string language;

		[Column(Type = DbType.String)]
		public string Body
		{
			get { return body; }
			set { body = value;  }
		}

		[Column(Type = DbType.String, AllowNull = true)]
		public string Language
		{
			get { return language; }
			set { language = value;  }
		}				

		#endregion

	}

	public static class Language
	{
		public const string CSharp = "CSharp";
	}
}
