#if NET20

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
    public sealed class TypeForwardedFromAttribute : Attribute
    {
        private readonly string assemblyFullName;

        public TypeForwardedFromAttribute(string assemblyFullName)
        {
            if (string.IsNullOrEmpty(assemblyFullName))
            {
                throw new ArgumentNullException(nameof(assemblyFullName));
            }
            this.assemblyFullName = assemblyFullName;
        }

        public string AssemblyFullName
        {
            get
            {
                return this.assemblyFullName;
            }
        }
    }
}

#endif