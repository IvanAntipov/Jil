using System;
using System.Collections.Generic;
using System.Linq;

namespace JilFork.Common
{
    /// <summary>
    /// Diagnostics
    /// </summary>
    public static class Diagnostics
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="forType"></param>
        /// <returns></returns>
        public static ClassInfo GetClassInfo(Type forType)
        {
            var recursive = Utils.FindRecursiveTypes(forType);
            var reusedTypes = Utils.FindReusedTypes(forType);
            
            var heavy = Utils.FindHeavyTypes(forType, recursive.Concat(reusedTypes).ToList(), ExtensionMethods.MaxTypeWeight);
            return new ClassInfo(reusedTypes, reusedTypes, heavy);
        }
        /// <summary>
        /// Class graph deserialization details
        /// </summary>
        public class ClassInfo
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="recursiveTypes"></param>
            /// <param name="reusedTypes"></param>
            /// <param name="heavyTypes"></param>
            public ClassInfo(IReadOnlyCollection<Type> recursiveTypes, IReadOnlyCollection<Type> reusedTypes, IReadOnlyCollection<Type> heavyTypes)
            {
                RecursiveTypes = recursiveTypes;
                ReusedTypes = reusedTypes;
                HeavyTypes = heavyTypes;
            }

            /// <summary>
            /// Recursive types
            /// </summary>
            public IReadOnlyCollection<Type> RecursiveTypes { get; private set; }
            /// <summary>
            /// Reused types
            /// </summary>
            public IReadOnlyCollection<Type> ReusedTypes { get; private set; }
            /// <summary>
            /// Heavy types
            /// </summary>
            public IReadOnlyCollection<Type> HeavyTypes { get; private set; }            
        }
    }
}