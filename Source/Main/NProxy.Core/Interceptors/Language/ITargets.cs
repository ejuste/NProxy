//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
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
namespace NProxy.Core.Interceptors.Language
{
    /// <summary>
    /// Defines the <c>Targets</c> verb.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    public interface ITargets<out T> : IFluent where T : class
    {
        /// <summary>
        /// Specifies an invocation target.
        /// </summary>
        /// <typeparam name="TTarget">The target type.</typeparam>
        IInvokes<T> Targets<TTarget>() where TTarget : class, new();

        /// <summary>
        /// Specifies an invocation target.
        /// </summary>
        /// <param name="target">The target object.</param>
        IInvokes<T> Targets(object target);

        /// <summary>
        /// Specifies an invocation target.
        /// </summary>
        /// <param name="invocationTarget">The invocation target.</param>
        IInvokes<T> Targets(IInvocationTarget invocationTarget);
    }
}
