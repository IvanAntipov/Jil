using System;

namespace JilFork
{
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class JilClassDirectiveAttribute : Attribute
    {
        /// <summary>
        /// Name of property to store raw json string
        /// </summary>
        public string RawPropertyName { get; }

        /// <summary>
        /// </summary>
        /// <param name="rawPropertyName"></param>
        public JilClassDirectiveAttribute(string rawPropertyName)
        {
            RawPropertyName = rawPropertyName;
        }
    }
}