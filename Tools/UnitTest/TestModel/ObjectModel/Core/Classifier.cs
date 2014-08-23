/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Xml.Serialization;
using Common.Extensions;
using Logging;
using Common.Persistent.ORMapping;

namespace UnitTest.TestModel
{
	[Table]
	[Serializable]
	public partial class Classifier : NameSpace
	{
		#region ctor

		public Classifier()
			: this(TableType.Classifier)
		{
			classifierType = ClassifierType.Default;
		}

		public Classifier(TableType type)
			: base(type)
		{
		}

		#endregion

		#region ORMapping Columns

		private bool isAbstract;

		[Column(Type = DbType.Boolean)]
		public bool IsAbstract
		{
			get { return isAbstract; }
			set { isAbstract = value;  }
		}

		private ClassifierType classifierType;
		[Column(Type = DbType.Int16, AllowNull = true)]
		public ClassifierType ClassifierType
		{
			get { return classifierType; }
			set { classifierType = value;  }
		}

		#endregion

		
	}
}
