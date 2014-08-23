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

namespace Common.Wpf.ViewModel
{
    [Serializable]
    public partial class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public ViewModelBase()
        {
            
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
            Dirty = false;
        }

        public virtual void Reset()
        {
            Dirty = false;
        }
        
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
    }
}
