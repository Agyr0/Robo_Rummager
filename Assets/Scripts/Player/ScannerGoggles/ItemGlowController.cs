using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class ItemGlowController : MonoBehaviour
{
    public DecalProjector decalProjector;
    private float value;
    private void Start()
    {
        
        StartCoroutine(FadeGlow());
    }

    public IEnumerator FadeGlow()
    {
        decalProjector.material.SetFloat("GlowValue", 1);
        value = decalProjector.material.GetFloat("GlowValue");

        while (true)
        {
            value = Mathf.Lerp(value, 0, Time.deltaTime);
            decalProjector.material.SetFloat("GlowValue", value);

            yield return null;
        }
    }
}
