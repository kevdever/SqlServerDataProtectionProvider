// Copyright (c) 2018 KevDever. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using SqlServerDataProtectionProvider.DataContext;
using System;

namespace SqlServerDataProtectionProvider.DataProtectionBuilderExtensions
{
    public static class DataProtectionExtensions
    {
        public static IDataProtectionBuilder PersistKeysToSqlServer(this IDataProtectionBuilder builder, Func<IDataProtectionContext> contextFactory)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            if (contextFactory is null)
                throw new ArgumentNullException(nameof(contextFactory));

            builder.Services.Configure<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new DataProtectionKeyRepository(contextFactory);
            });
            return builder;
        }
    }
}
