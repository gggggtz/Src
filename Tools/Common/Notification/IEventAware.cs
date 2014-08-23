/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notification
{
    public enum Events
    {
        None = 0,
        Added = 1,
        Deleted = 2,
        Changed = 4,
        MemberAdded = 8,
        MemberDeleted = 16,
    }
    public interface IEventAware
    {
        void SendNotifications();
    }
}
