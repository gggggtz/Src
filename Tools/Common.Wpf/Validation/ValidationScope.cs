/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using Common.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wpf.Validation
{
    public class ValidationManager
    {
        public bool ForceSync { get; set; }

        private ValidationManager()
        {
            ForceSync = false;
        }
    }
    public class ValidationScope : IDisposable
    {
        bool force = false;
        private bool sync = false;
        private ValidatableViewModel scope;

        public ValidationScope(ValidatableViewModel element, bool force)
            : this(element, force, Singleton<ValidationManager>.Instance.ForceSync)
        {
        }

        public ValidationScope(ValidatableViewModel element, bool force, bool sync)
        {
            this.scope = element;
            this.force = force;
            this.sync = sync;
        }


        public void Dispose()
        {
            if (sync)
            {
                scope.Validate(force);
            }
            else
            {
                Task.Run(() =>
                {
                    scope.Validate(force);
                });
            }
        }
    }
}
