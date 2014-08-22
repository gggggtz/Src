/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using Common.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wpf.ViewModel
{
    public class DataViewModel : ViewModelBase, IEventAware
    {
        public static event Action<ViewModelBase> ElementAdded;
        public event Action ElementChanged;
        public event Action ElementDeleted;
        public event Action<ViewModelBase> MemberAdded;
        public event Action<ViewModelBase> MemberDeleted;

        protected Events events;

        protected EventContext eventContext
        {
            get
            {
                return Singleton<EventContext.EventContextManager>.Instance.EventContext;
            }
        }

        public void NotifyDelete()
        {
            if (eventContext.IsEnabled())
            {
                var p = GetParent() as DataViewModel;
                if (p != null)
                {
                    if ((p.events & Events.Deleted) == Events.None)
                    {
                        events |= Events.MemberDeleted;
                        eventContext.Add(this);
                    }
                }
                else
                {
                    events |= Events.Deleted;
                    eventContext.Add(this);
                }
            }
        }

        public void NotifyAdded()
        {
            if (eventContext.IsEnabled())
            {
                var p = GetParent() as DataViewModel;
                if (p != null)
                {
                    if ((p.events & Events.Added) == Events.None)
                    {
                        events |= Events.MemberAdded;
                        eventContext.Add(this);
                    }
                }
                else
                {
                    events |= Events.Added;
                    eventContext.Add(this);
                }
            }
        }

        public void NotifyChanged()
        {
            if (eventContext.IsEnabled())
            {
                events |= Events.Changed;
                eventContext.Add(this);
            }
        }

        public virtual void SendNotifications()
        {
            lock (this)
            {
                if ((events & Events.Added) != Events.None)
                {
                    eventContext.RecordEvent(Events.Added, this);
                    if (ElementAdded != null)
                    {
                        ElementAdded(this);
                    }
                }
                if ((events & Events.Changed) != Events.None)
                {
                    eventContext.RecordEvent(Events.Changed, this);
                    if (ElementChanged != null)
                    {
                        ElementChanged();
                    }
                }
                if ((events & Events.Deleted) != Events.None)
                {
                    eventContext.RecordEvent(Events.Deleted, this);
                    if (ElementDeleted != null)
                    {
                        ElementDeleted();
                    }
                }
                if ((events & Events.MemberAdded) != Events.None)
                {
                    eventContext.RecordEvent(Events.MemberAdded, this);
                    var o = GetParent() as DataViewModel;
                    if (o != null && o.MemberAdded != null)
                    {
                        o.MemberAdded(this);
                    }
                }
                if ((events & Events.MemberDeleted) != Events.None)
                {
                    eventContext.RecordEvent(Events.MemberDeleted, this);
                    var o = GetParent() as DataViewModel;
                    if (o != null && o.MemberDeleted != null)
                    {
                        o.MemberDeleted(this);
                    }
                }
                events = Events.None;
            }
        }
    }
}
