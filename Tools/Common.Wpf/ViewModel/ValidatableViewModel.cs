using Common.Wpf.Validation;
/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;

namespace Common.Wpf.ViewModel
{
    public enum ValidationStatus
    {
        Valid,
        Invalid,
        NeedsValidation
    }
    public partial class ValidatableViewModel : PropertizedViewModel
    {

        #region Validation Properties

        protected static Hashtable validationProperties = new Hashtable();
                
        #endregion

        #region Validate

        public event Action<ViewModelBase> ValidationCompleted;

        public ValidationStatus ValidationStatus { protected set; get; }

        private volatile bool validating = false;

        public bool Validating { get { return validating; } }

        public void Validate(bool force = false)
        {
            if ((ValidationStatus != ValidationStatus.Valid || force) && !validating)
            {
                validating = true;

                ClearErrors();
                ValidationStatus = ValidationStatus.Valid;
                ValidateRules();

                foreach (var p in changedProperties)
                {
                    if (p.Value.NotifyParent)
                    {
                        ValidateParent();
                        break;
                    }
                }
                if (ValidationCompleted != null)
                {
                    ValidationCompleted(this);
                }
            }
        }

        private void ValidateRules()
        {
            lock(changedProperties)
            {
                changedProperties.Values.Foreach(p =>
                    {
                        p.Rules.Foreach(r =>
                            {
                                var vr = r.Validate(p.GetValue(), System.Threading.Thread.CurrentThread.CurrentCulture);
                                if(!vr.IsValid)
                                {
                                    if(!validationErrors.ContainsKey(p.PropertyName))
                                    {
                                        validationErrors.Add(p.PropertyName,new ErrorIfo());
                                    }
                                    validationErrors[p.PropertyName].Errors.Add(r, vr);
                                    
                                    ValidationStatus = ValidationStatus.Invalid;
                                }
                            });
                    });
            }
        }

        private void ValidateParent()
        {
            var p = GetParent() as ValidatableViewModel;
            if(p != null)
            {
                p.Validate();
            }
        }
        
        #endregion

        #region Error

        protected Dictionary<string, ErrorIfo> validationErrors = new Dictionary<string, ErrorIfo>();

        protected virtual void ClearErrors()
        {
            validationErrors.Clear();
        }
        
        #endregion

        #region Overrides

        public override void Reset()
        {
            ClearErrors();
            base.Reset();
        }
        protected override void PropChanged(string prop)
        {
            lock (changedProperties)
            {
                if (!changedProperties.ContainsKey(prop))
                {
                    var ps = GetProps();
                    if (ps.ContainsKey(prop))
                    {
                        var p = ps[prop];
                        if (ValidationStatus == ValidationStatus.Valid)
                        {
                            if (p.Rules.Count > 0)
                            {
                                ValidationStatus = ValidationStatus.NeedsValidation;
                            }
                        }
                        base.PropChanged(prop);
                    }
                }
            }
        }
        protected override void ReleaseManagedResource()
        {
            ClearErrors();
            ValidationCompleted = null;
            base.ReleaseManagedResource();
        }

        #endregion
    }
}
