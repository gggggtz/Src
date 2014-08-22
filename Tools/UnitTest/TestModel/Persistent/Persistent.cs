/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using Common.Persistent;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Unitest.TestModel
{
	public abstract class Persistent : IEntity
	{
		[XmlIgnore]
		public PersistentState PersistentState { get; set; }

		public Persistent()
		{
			PersistentState = PersistentState.Added;
		}

		public virtual void LoadCompleted()
		{
			PersistentState = PersistentState.Loaded;
		}

	}
}
