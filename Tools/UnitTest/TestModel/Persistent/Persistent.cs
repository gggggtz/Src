/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using Common.Persistent;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UnitTest.TestModel
{
	public class Persistent : Entity
	{
		[XmlIgnore]
		public PersistentState PersistentState { get; set; }

		public Persistent()
		{
			PersistentState = PersistentState.Added;
		}

		public override void LoadCompleted()
		{
			PersistentState = PersistentState.Loaded;
		}

	}
}
