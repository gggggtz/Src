/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Extensions;
using Common.Persistent.ORMapping;

namespace Unitest.TestModel
{
	[Table]
	[Serializable]
	public partial class Schema : NameSpace
	{
		public Schema() : base(TableType.Schema)
		{
		}
	}
}
