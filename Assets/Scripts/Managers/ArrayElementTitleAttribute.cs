
using UnityEngine;

namespace Agyr.CustomAttributes
{
#if UNITY_EDITOR
    public class ArrayElementTitleAttribute : PropertyAttribute
    {
        public string Varname;
        public ArrayElementTitleAttribute(string ElementTitleVar)
        {
            Varname = ElementTitleVar;
        }
    }
#endif
}