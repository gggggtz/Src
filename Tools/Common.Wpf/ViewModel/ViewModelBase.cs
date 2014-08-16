/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Common.Extensions;
using Common.ObjectHistory;
using Common.Wpf.View;
using System.Collections;
using System.Collections.Specialized;
using Common.Wpf.Validation;

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

    [Serializable]
    public abstract partial class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public ViewModelBase()
        {
            InitializeProperties();

            PropertyChanged += (s, e) =>
                {
                    PropChanged(e.PropertyName);
                };
        }

        public IView View { get; set; }

        #region INotifyPropertyChanged Implementation

        public void ChangeAndNotify<T>(ref T field, T value, Expression<Func<T>> memberExpression)
        {
            PropertyChanged.ChangeAndNotify<T>(this, ref field, value, memberExpression);
            Dirty = true;
        }

        public void ChangeAndNotifyHistory<T>(HistoryableProperty<T> field, T value, Expression<Func<T>> memberExpression)
        {
            PropertyChanged.ChangeAndNotifyHistory<T>(this, field, value, memberExpression);
            Dirty = true;
        }

        public void Notify<T>(Expression<Func<T>> memberExpression)
        {
            PropertyChanged.Notify<T>(this, memberExpression);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Dirty

        private bool dirty = false;

        /// <summary>
        /// Gets / sets the Title value
        /// </summary>
        public virtual bool Dirty
        {
            get { return dirty; }
            set
            {
                if (dirty != value)
                {
                    dirty = value;
                    Notify<bool>(() => Dirty);

                    if (value)
                    {
                        var p = GetParent();
                        if (p != null)
                        {
                            p.Dirty = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region IDisposable Implemenation

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    ReleaseManagedResource();
                }
                ReleaseUnManagedResource();
                disposed = true;
            }
        }

        protected virtual void ReleaseManagedResource()
        {
            ClearErrors();
            ValidationCompleted = null;
        }

        protected virtual void ReleaseUnManagedResource()
        {

        }

        ~ViewModelBase()
        {
            Dispose(false);
        }

        #endregion

        #region Initialize

        public virtual void Initialize()
        {
            InitializeResource();
            InitializeCommand();
        }

        protected virtual void InitializeResource()
        {
        }

        protected virtual void InitializeCommand()
        {
        }

        #endregion

        #region Operation

        public virtual void Save()
        {
            changedProperties.Clear();
            Dirty = false;
        }

        public virtual void Reset()
        {
            changedProperties.Clear();
            ClearErrors();
            Dirty = false;
        }

        public void DispatcherInvoker(Action act, bool async = false)
        {
            //var disp = System.Windows.Threading.Dispatcher.CurrentDispatcher;
            var disp = System.Windows.Application.Current.Dispatcher;
            if (disp != null)
            {
                if (async)
                {
                    disp.BeginInvoke((Action)(() => act()));
                }
                else
                {
                    disp.Invoke((Action)(() => act()));
                }
            }
            else
            {
                act();
            }
        }

        #endregion

        #region Property Handling

        #region Proected

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
        protected virtual void ProvideCollectionProps()
        {
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
        protected virtual string GetParentPropertyName()
        {
            return string.Empty;
        }
        protected virtual Dictionary<string, Property> ProvideProps()
        {
            return new Dictionary<string, Property>(); ;
        }

        protected virtual void PropChanged(string prop)
        {
            lock (changedProperties)
            {
                if (!changedProperties.ContainsKey(prop))
                {
                    var ps = GetProps();
                    if (ps.ContainsKey(prop))
                    {
                        var p = ps[prop];
                        Dirty = true;
                        changedProperties.Add(prop, p);
                        if (p.NotifyParent)
                        {
                            var parent = GetParent();
                            if (parent != null)
                            {
                                parent.PropChanged(GetParentPropertyName());
                            }
                        }
                        if (ValidationStatus == ValidationStatus.Valid)
                        {
                            if (p.Rules.Count > 0)
                            {
                                ValidationStatus = ValidationStatus.NeedsValidation;
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

        #region Public

        public ViewModelBase GetTopLevelParent()
        {
            var p = GetParent();
            if (p == null)
            {
                return this;
            }
            else
            {
                return p.GetTopLevelParent();
            }
        }

        public virtual ViewModelBase GetParent()
        {
            return null;
        }

        #endregion

        #endregion
    }
}
