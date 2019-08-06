using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DbUpTemplate.src.env
{
    /// <summary>
    /// Typesafe enum to check the environment.
    /// </summary>
    internal sealed class EnvironmentEnum
    {
        public static EnvironmentEnum Development { get; } = new EnvironmentEnum("Development");
        public static EnvironmentEnum Testing { get; } = new EnvironmentEnum("Testing");
        public static EnvironmentEnum Staging { get; } = new EnvironmentEnum("Staging");
        public static EnvironmentEnum Production { get; } = new EnvironmentEnum("Production");
        public string Name { get; }
        private EnvironmentEnum(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;

        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="environment">The environment as an string.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified environment]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">environment</exception>
        public static bool Contains(string environment)
        {
            if (String.IsNullOrWhiteSpace(environment))
                throw new ArgumentNullException(nameof(environment));
            
            return GetValues().Any(env => env.Name.Equals(environment));
        }

        private static IReadOnlyList<EnvironmentEnum> GetValues() => typeof(EnvironmentEnum)
                                                            .GetProperties(BindingFlags.Public | BindingFlags.Static)
                                                            .Select(property => (EnvironmentEnum)property.GetValue(null, null))
                                                            .ToList();
    }
}
