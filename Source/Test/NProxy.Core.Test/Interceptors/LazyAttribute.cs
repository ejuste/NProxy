﻿//
// Copyright © Martin Tamme
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Reflection;
using NProxy.Core.Interceptors;

namespace NProxy.Core.Test.Interceptors
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    internal sealed class LazyAttribute : Attribute, IInterceptionBehavior
    {
        #region IInterceptionBehavior Members

        public void Apply(MemberInfo memberInfo, ICollection<IInterceptor> interceptors)
        {
            interceptors.Add(LazyInterceptor.Instance);
        }

        public void Validate(MemberInfo memberInfo)
        {
        }

        #endregion
    }
}