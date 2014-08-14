﻿/// <copyright>
/// Copyright ©  2014 Microsoft Corporation. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// This interface supports 
    /// </summary>
    public interface IPersistable
    {
		/// <summary>
		/// Identify is the element is changed or not
		/// </summary>
        bool IsDirty { get; set; }
		/// <summary>
		/// Reset the element from the storage
		/// </summary>
        void Reset();
		/// <summary>
		/// Save the element
		/// </summary>
        void Save();
    }
}
