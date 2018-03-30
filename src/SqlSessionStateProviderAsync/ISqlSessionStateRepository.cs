﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.SessionState
{
    using System;
    using System.Threading.Tasks;
    using System.Web.SessionState;

    internal interface ISqlSessionStateRepository
    {
        void CreateSessionStateTable();

        void DeleteExpiredSessions();

        Task<SessionItem> GetSessionStateItemAsync(string id, bool exclusive);

        Task CreateOrUpdateSessionStateItemAsync(bool newItem, string id, byte[] buf, int length, int timeout, int lockCookie, int orginalStreamLen);

        Task ResetSessionItemTimeoutAsync(string id);

        Task RemoveSessionItemAsync(string id, object lockId);

        Task ReleaseSessionItemAsync(string id, object lockId);

        Task CreateUninitializedSessionItemAsync(string id, int length, byte[] buf, int timeout);
    }

    class SessionItem
    {
        public SessionItem(byte[] item, bool locked, TimeSpan lockAge, object lockId,
            SessionStateActions actions)
        {
            Item = item;
            Locked = locked;
            LockAge = lockAge;
            LockId = lockId;
            Actions = actions;
        }
        
        public byte[] Item { get; private set; }

        public bool Locked { get; private set; }
        
        public TimeSpan LockAge { get; private set; }
        
        public object LockId { get; private set; }

        public SessionStateActions Actions { get; private set; }
    }
}
