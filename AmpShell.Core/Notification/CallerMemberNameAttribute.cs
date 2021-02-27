// Copyright (c) Microsoft Corporation. All rights reserved.
#if NET20 || NET40

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public sealed class CallerMemberNameAttribute : Attribute
    {
        public CallerMemberNameAttribute()
        {
        }
    }
}

#endif