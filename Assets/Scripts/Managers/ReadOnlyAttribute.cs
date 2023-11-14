using System;
using UnityEditor;
using UnityEngine;

namespace Agyr.CustomAttributes
{
#if UNITY_EDITOR
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }
#endif
}