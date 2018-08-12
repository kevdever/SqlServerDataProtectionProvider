// Copyright (c) 2018 KevDever. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.DataProtection.Repositories;
using SqlServerDataProtectionProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SqlServerDataProtectionProvider.DataContext
{
    /// <summary>
    /// This implementation of the IXmlRepository is modeled on the built-in RedisXmlRepository: https://github.com/aspnet/DataProtection/blob/master/src/Microsoft.AspNetCore.DataProtection.Redis/RedisXmlRepository.cs
    /// </summary>
    public class DataProtectionKeyRepository : IXmlRepository
    {
        private readonly Func<IDataProtectionContext> _contextFactory;

        public DataProtectionKeyRepository(Func<IDataProtectionContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IReadOnlyCollection<XElement> GetAllElements() => GetAllElementsCore().ToList().AsReadOnly();

        /// <summary>
        /// See the comments in the Redis file mentioned above about failure modes.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<XElement> GetAllElementsCore()
        {
            using (var context = _contextFactory())
            {
                foreach (var element in context.DataProtectionKeys)
                    yield return XElement.Parse(element.XmlData);
            }
        }

        /// <summary>
        /// If a key is already in the database, then update it; else, add it.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="friendlyName"></param>
        public void StoreElement(XElement element, string friendlyName)
        {
            using (var context = _contextFactory())
            {
                var entity = context.DataProtectionKeys.Find(friendlyName);
                if (entity is null)
                {
                    entity = new DataProtectionKey
                    {
                        FriendlyName = friendlyName,
                    };
                    context.DataProtectionKeys.Add(entity);
                }

                entity.XmlData = element.ToString(SaveOptions.DisableFormatting);
                context.SaveChanges();
            }
        }
    }
}
