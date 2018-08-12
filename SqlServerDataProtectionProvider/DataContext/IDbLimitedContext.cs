// Copyright (c) 2018 KevDever. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDataProtectionProvider.DataContext
{
    /// <summary>
    /// An interface for DbContext providing a limited subset of methods needed for the DataProtectionRepository
    /// </summary>
    public interface IDbLimitedContext : IDisposable
    {
        int SaveChanges();
        T Find<T>(params object[] keys) where T : class;
    }
}
