/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Wpf.Validation;
using System.Collections;
using System.Collections.Specialized;


namespace Common.Wpf.ViewModel
{
    public class Property
    {
        public string PropertyName { get; private set; }
        public bool NotifyParent { get; set; }

        private Expression<Func<object>> memberExpression;
        public Property(string propName, Expression<Func<object>> mExpression, bool notifyParent = true)
        {
            PropertyName = propName;
            NotifyParent = notifyParent;
            memberExpression = mExpression;
        }

        public List<ValidationRuleBase> Rules = new List<ValidationRuleBase>();

        public object GetValue()
        {
            return memberExpression.Compile()();
        }
    }
    public class PropertizedViewModel : ViewModelBase
    {
        public PropertizedViewModel()
        {
            InitializeProperties();

            PropertyChanged += (s, e) =>
            {
                PropChanged(e.PropertyName);
            };
        }

        #region Properties

        protected static Hashtable properties = new Hashtable();
        protected void InitializeProperties()
        {
            lock (properties)
            {
                if (!properties.ContainsKey(this.GetType()))
                {
                    properties.Add(this.GetType(), ProvideProps());
                }
            }
            ProvideCollectionProps();
        }
        protected Dictionary<string, Property> GetProps()
        {
            if (properties.ContainsKey(this.GetType()))
            {
                return properties[this.GetType()] as Dictionary<string, Property>;
            }
            throw new Exception("Properties not initialized for " + this.GetType().ToString());
        }
        protected Dictionary<string, Property> changedProperties = new Dictionary<string, Property>();

        #region Virtual Methods
        protected virtual string GetParentPropertyName()
        {
            return string.Empty;
        }
        protected virtual Dictionary<string, Property> ProvideProps()
        {
            return new Dictionary<string, Property>(); ;
        }
        protected virtual void ProvideCollectionProps()
        {
        }
        protected virtual void PropChanged(string prop)
        {
            lock (changedProperties)
            {
                if (!changedProperties.ContainsKey(prop))
                {
                    var ps = GetProps() ;
                    if (ps.ContainsKey(prop))
                    {
                        var p = ps[prop];
                        Dirty = true;
                        changedProperties.Add(prop, p);
                        if (p.NotifyParent)
                        {
                            var parent = GetParent() as PropertizedViewModel;
                            if (parent != null)
                            {
                                parent.PropChanged(GetParentPropertyName());
                            }
                        }                        
                    }
                }
            }
        }
        protected virtual void RegisterCollectionChanged<T>(INotifyCollectionChanged prop, string propertyName)
        {
            prop.CollectionChanged += (s, e) => { this.PropChanged(propertyName); };
        }

        #endregion
        #endregion

        #region Overrides
        public override void Save()
        {
            changedProperties.Clear();
            base.Save();
        }

        public virtual void Reset()
        {
            changedProperties.Clear();
            base.Reset();
        }

        #endregion
    }
}
