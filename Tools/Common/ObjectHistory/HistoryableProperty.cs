/// <copyright>
/// Copyright ©  2014 Microsoft Corporation. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Common.ObjectHistory
{
	public class HistoryableProperty<T>
	{
		private T value;

		public HistoryableProperty(T defaultValue)
		{
			value = defaultValue;
		}

		public T GetValue()
		{
			lock (this)
			{
				return value;
			}
		}

		public void SetValue(T value, Action notify = null)
		{
			lock (this)
			{
                //var historyContext = Singleton<HistoryContextManager>.Instance.Context;
                //if (historyContext != null)
                //{
                //    T oldValue = this.value;
                //    T newValue = value;
                //    historyContext.AddHistory(
                //        () => { this.value = oldValue; if (notify != null) notify(); },
                //        () => { this.value = newValue; if (notify != null) notify(); }
                //    );
                //}

				this.value = value;
			}
		}
	}
}
