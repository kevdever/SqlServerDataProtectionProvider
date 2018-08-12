// Copyright (c) 2018 KevDever. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using SqlServerDataProtectionProvider.Model;

namespace SqlServerDataProtectionProvider.DataContext
{
    /// <summary>
    /// An interface that your database context needs to implement to use this library
    /// </summary>
    public interface IDataProtectionContext : IDbLimitedContext
    {
        DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}
