
#if UNITY_EDITOR
using UnityEngine;

namespace Agyr.CustomAttributes
{
    public class ArrayElementTitleAttribute : PropertyAttribute
    {
        public string Varname;
        public ArrayElementTitleAttribute(string ElementTitleVar)
        {
            Varname = ElementTitleVar;
        }
    }
}
#endif