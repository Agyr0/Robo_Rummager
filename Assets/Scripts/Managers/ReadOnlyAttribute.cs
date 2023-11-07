#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Agyr.CustomAttributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }
}
#endif