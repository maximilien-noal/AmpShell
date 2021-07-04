// Copyright (c) Microsoft Corporation. All rights reserved.
#if NET20

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class CallerMemberNameAttribute : Attribute
    {
        public CallerMemberNameAttribute()
        {
        }
    }
}

#endif