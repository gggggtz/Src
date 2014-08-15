/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common.ObjectHistory;
using System.Collections.Specialized;
using System.Windows.Controls;
using Common.Wpf.Validation;

namespace Common.Wpf.ViewModel
{
    public enum ValidationStatus
    {
        Valid,
        Invalid,
        NeedsValidation
    }


	public abstract class ValidationViewModelBase : ViewModelBase
    {
        public ValidationViewModelBase()
        {
            Errors = new List<ValidationError>();
        }

        #region Validation

        public event Action<ValidationViewModelBase> ValidationCompleted;        
        
        public List<ValidationError> Errors { get; protected set; }

        public ValidationStatus ValidationStatus { protected set; get; }

		private volatile bool validating = false;
		public bool Validating { get { return validating; } }

		public void Validate(bool force = false)
		{
            if ((ValidationStatus != ValidationStatus.Valid || force) && !validating)
            {
                validating = true;

                ClearErrors();
                DoValidation(force);
                ValidationStatus = ValidationStatus.Valid;

                foreach (var p in changedProperties)
                {
                    if (p.Value.NotifyParent)
                    {
                        ValidateParent();
                        break;
                    }
                }
            }

			if (ValidationCompleted != null)
			{
				ValidationCompleted(this);
			}
		}

		protected virtual void ValidateParent()
		{
			var parent = GetParent() as ValidationViewModelBase;
			if (parent != null)
			{
				parent.Validate();
			}
		}

		protected virtual void DoValidation(bool force)
		{
            Singleton<ValidationRuleManager>.Instance.Rules.ForEach(r =>
                {
                    if (IsRuleApplicable(r))
                    {
                        ValidateRule(r);
                    }
                });
		}

		public virtual void ClearErrors()
		{
            Errors.Clear();
		}

        public abstract bool IsRuleApplicable(ValidationRuleBase rule);

        public abstract void ValidateRule(ValidationRuleBase rule);
        
        #endregion

        #region Overrides

        public override void Save()
		{
			changedProperties.Clear();
			base.Save();
		}

        public override void Reset()
        {
            changedProperties.Clear();
            ClearErrors();
            base.Reset();
        }

        protected override void ReleaseManagedResource()
        {
            ClearErrors();
            ValidationCompleted = null;
            base.ReleaseManagedResource();
        }

        protected override void PropChanged(string prop)
		{
            if (ValidationStatus == ValidationStatus.Valid)
            {
                var ps = GetProps();
                if (ps.ContainsKey(prop))
                {
                    var p = ps[prop];
                    if (p != null && p.ImpactValidation)
                    {
                        ValidationStatus = ValidationStatus.NeedsValidation;
                    }
                }
            }
			base.PropChanged(prop);
        }

        #endregion
    }
}
