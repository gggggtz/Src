using Common.Notification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    public interface IEntity
    {
        void LoadCompleted();
    }

    public enum PersistentState
    {
        None = 0,
        Loaded = 1,
        Added = 2,
        Changed = 4,
    }

    public abstract class Entity : IEntity, IEventAware
    {
        public Entity()
        {
            PersistentState = PersistentState.Added;
        }

        #region Events

        public static event Action<Entity> ElementAdded;
        public event Action ElementChanged;
        public event Action ElementDeleted;
        public event Action<Entity> MemberAdded;
        public event Action<Entity> MemberDeleted;

        protected Events events;

        protected EventContext eventContext
        {
            get
            {
                return Singleton<EventContext.EventContextManager>.Instance.EventContext;
            }
        }

        #endregion

        #region PersistentState

        [XmlIgnore]
        public PersistentState PersistentState { get; protected set; }

        public virtual void LoadCompleted()
        {
            PersistentState = PersistentState.Loaded;
        }

        public virtual Entity GetParent()
        {
            return null;
        }

        #endregion

        #region Notification

        public void NotifyDelete()
        {
            if (eventContext.IsEnabled())
            {
                var p = GetParent();
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
                var p = GetParent();
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
                    var o = GetParent();
                    if (o != null && o.MemberAdded != null)
                    {
                        o.MemberAdded(this);
                    }
                }
                if ((events & Events.MemberDeleted) != Events.None)
                {
                    eventContext.RecordEvent(Events.MemberDeleted, this);
                    var o = GetParent();
                    if (o != null && o.MemberDeleted != null)
                    {
                        o.MemberDeleted(this);
                    }
                }
                events = Events.None;
            }
        }

        #endregion
    }

    
}
