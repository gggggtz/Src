/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>using System;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using Logging;
using Common.Persistent.ORMapping;

namespace UnitTest.TestModel
{
	[Table]
	[Serializable]
	public partial class NameSpace : ModelElement
	{

		public NameSpace()
			: this(TableType.NameSpace)
		{
		}

		public NameSpace(TableType type)
			: base(type)
		{
		}


	}
}
