/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>using System;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Serialization;
using Common;
using Logging;
using Common.Persistent.ORMapping;
using Common.Notification;

namespace Unitest.TestModel
{
	[Table]
	[Serializable]
	public partial class Element : Persistent
	{
		public Element()
			: this(TableType.Element)
		{

		}

		public Element(TableType type)
		{
			Id = Guid.NewGuid();
			Discriminator = type;
			LastModified = DateTime.Now;

			state = ElementState.Normal;
			status = ValidationState.Valid;
			Revision = 1;
		}

		#region ORMapping Columns

		private Guid? reservedBy;
		private ElementState state;
		private ValidationState status;

		[Column(Type = DbType.Int32)]
		public ValidationState Status
		{
			get { return status; }
			set { status = value;  }
		}

		[Column(IsPrimaryKey = true, Type = DbType.Guid)]
		public System.Guid Id { get; set; }

		[Column(Type = DbType.Int32)]
		public TableType Discriminator { get; set; }

		/// <summary>
		/// This property is used to record the user id who is changing the element.
		/// </summary>
		[Column(Type = DbType.Guid, AllowNull = true)]
		public Guid? ReservedBy
		{
			get { return reservedBy; }
			set { reservedBy = value;  }
		}

		private bool updateRevision = false;

		[XmlIgnore]
		[Column(Type = DbType.Int64, AllowNull = true)]
		public Int64 Revision { get; set; }

		[Column(Type = DbType.DateTime, AllowNull = true)]
		public DateTime LastModified { get; set; }

		/// <summary>
		/// Indicate whether the element is normal, added, changed or deleted
		/// </summary>
		[Column(Type = DbType.Int16)]
		public ElementState State
		{
			get
			{
				return state;
			}
			set
			{
				var oldState = state;

				state = value;

				if (oldState != state)
				{
					
				}
			}
		}

		#endregion

	}
}
