/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Notification
{
    
    public class EventContext
    {
        public class EventContextManager
        {
            private ThreadLocal<EventContext> eventContext = new ThreadLocal<EventContext>(() => new EventContext());

            private EventContextManager()
            {
            }

            public EventContext EventContext
            {
                get
                {
                    return eventContext.Value;
                }
            }

        }

        private Queue<IEventAware> eventQueue = new Queue<IEventAware>();
        private Dictionary<Events, List<IEventAware>> eventCounter = new Dictionary<Events, List<IEventAware>>();
        private bool enabled = false;
        private bool recordEnabled = false;

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }

        public void RecordEvent(bool enable)
        {
            recordEnabled = enable;
        }

        private EventContext()
        {
            foreach (Events e in Enum.GetValues(typeof(Events)))
            {
                eventCounter[e] = new List<IEventAware>();
            }
        }

        public bool IsEnabled()
        {
            return enabled;
        }

        public void Add(IEventAware element)
        {
            if (enabled)
            {
                if (!eventQueue.Contains(element))
                {
                    eventQueue.Enqueue(element);
                }
            }
        }

        public void SendNotifications()
        {
            while (eventQueue.Count > 0)
            {
                var element = eventQueue.Dequeue();
                element.SendNotifications();
            }
        }

        public void RecordEvent(Events e, IEventAware element)
        {
            if (recordEnabled)
            {
                eventCounter[e].Add(element);
            }
        }

        public void ClearEventCounter()
        {
            foreach (Events e in Enum.GetValues(typeof(Events)))
            {
                eventCounter[e].Clear();
            }
        }

        public int GetEventCount(Events e)
        {
            return eventCounter[e].Count;
        }
    }

    public class EventContextScope : IDisposable
    {
        public EventContextScope(bool recordEvent = false)
        {
            Singleton<EventContext.EventContextManager>.Instance.EventContext.ClearEventCounter();
            Singleton<EventContext.EventContextManager>.Instance.EventContext.Enable();
            Singleton<EventContext.EventContextManager>.Instance.EventContext.RecordEvent(recordEvent);
        }

        public void Dispose()
        {
            Singleton<EventContext.EventContextManager>.Instance.EventContext.Disable();
            Singleton<EventContext.EventContextManager>.Instance.EventContext.SendNotifications();
            Singleton<EventContext.EventContextManager>.Instance.EventContext.RecordEvent(false);
        }
    }
}
