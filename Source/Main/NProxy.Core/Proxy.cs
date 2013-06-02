﻿//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Reflection;
using NProxy.Core.Internal.Descriptors;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy.
    /// </summary>
    internal class Proxy : IProxy
    {
        /// <summary>
        /// The proxy descriptor.
        /// </summary>
        private readonly IDescriptor _descriptor;

        /// <summary>
        /// The type.
        /// </summary>
        private readonly Type _type;

        /// <summary>
        /// The event informations.
        /// </summary>
        private readonly ICollection<EventInfo> _eventInfos;

        /// <summary>
        /// The property informations.
        /// </summary>
        private readonly ICollection<PropertyInfo> _propertyInfos;

        /// <summary>
        /// The method informations.
        /// </summary>
        private readonly ICollection<MethodInfo> _methodInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="Proxy"/> class.
        /// </summary>
        /// <param name="descriptor">The proxy descriptor.</param>
        /// <param name="type">The type.</param>
        /// <param name="eventInfos">The event informations.</param>
        /// <param name="propertyInfos">The property informations.</param>
        /// <param name="methodInfos">The method informations.</param>
        public Proxy(IDescriptor descriptor, Type type, ICollection<EventInfo> eventInfos, ICollection<PropertyInfo> propertyInfos, ICollection<MethodInfo> methodInfos)
        {
            if (descriptor == null)
                throw new ArgumentNullException("descriptor");

            if (type == null)
                throw new ArgumentNullException("type");

            _descriptor = descriptor;
            _type = type;
            _eventInfos = eventInfos;
            _propertyInfos = propertyInfos;
            _methodInfos = methodInfos;
        }

        #region IProxy Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _descriptor.DeclaringType; }
        }

        /// <inheritdoc/>
        public TInterface Cast<TInterface>(object instance) where TInterface : class
        {
            return _descriptor.Cast<TInterface>(instance);
        }

        /// <inheritdoc/>
        public object CreateInstance(IInvocationHandler invocationHandler, params object[] arguments)
        {
            if (invocationHandler == null)
                throw new ArgumentNullException("invocationHandler");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            var constructorArguments = new List<object> {invocationHandler};

            constructorArguments.AddRange(arguments);

            return _descriptor.CreateInstance(_type, constructorArguments.ToArray());
        }

        /// <inheritdoc/>
        public IEnumerable<EventInfo> GetInterceptedEvents()
        {
            return _eventInfos;
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyInfo> GetInterceptedProperties()
        {
            return _propertyInfos;
        }

        /// <inheritdoc/>
        public IEnumerable<MethodInfo> GetInterceptedMethods()
        {
            return _methodInfos;
        }

        #endregion
    }

    /// <summary>
    /// Represents a proxy.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    internal sealed class Proxy<T> : IProxy<T> where T : class
    {
        /// <summary>
        /// The proxy.
        /// </summary>
        private readonly IProxy _proxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="Proxy{T}"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        public Proxy(IProxy proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            _proxy = proxy;
        }

        #region IProxy Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _proxy.DeclaringType; }
        }

        /// <inheritdoc/>
        public TInterface Cast<TInterface>(object instance) where TInterface : class
        {
            return _proxy.Cast<TInterface>(instance);
        }

        /// <inheritdoc/>
        object IProxy.CreateInstance(IInvocationHandler invocationHandler, params object[] arguments)
        {
            return _proxy.CreateInstance(invocationHandler, arguments);
        }

        /// <inheritdoc/>
        public IEnumerable<EventInfo> GetInterceptedEvents()
        {
            return _proxy.GetInterceptedEvents();
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyInfo> GetInterceptedProperties()
        {
            return _proxy.GetInterceptedProperties();
        }

        /// <inheritdoc/>
        public IEnumerable<MethodInfo> GetInterceptedMethods()
        {
            return _proxy.GetInterceptedMethods();
        }

        #endregion

        #region IProxy<T> Members

        /// <inheritdoc/>
        public T CreateInstance(IInvocationHandler invocationHandler, params object[] arguments)
        {
            return (T) _proxy.CreateInstance(invocationHandler, arguments);
        }

        #endregion
    }
}