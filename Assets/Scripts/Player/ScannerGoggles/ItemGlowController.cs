using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class ItemGlowController : MonoBehaviour
{
    private DecalProjector decalProjector;
    [SerializeField]
    private float fadeIn = .25f;
    [SerializeField]
    private float fadeOut = .75f;
    private float startVal = 0f;
    private float midVal = 1f;
    private float endVal = 0f;

    public bool running, toggle = false;

    private void Start()
    {
        decalProjector = gameObject.transform.GetComponentInChildren<DecalProjector>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.TOGGLE_SCANNER, ToggleGlow);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.TOGGLE_SCANNER, ToggleGlow);
    }

    //If player closes goggles when still fading cut to endVal
    private void ToggleGlow()
    {
        toggle = !toggle;
        if(!toggle)
        {
            StopCoroutine(FadeGlow());
            decalProjector.fadeFactor = endVal;
        }

    }

    public IEnumerator FadeGlow()
    {
        if (!running)
        {

            running = !running;

            float t = 0f;
            //Fade in fast
            while (t < fadeIn && toggle)
            {
                decalProjector.fadeFactor = Mathf.Lerp(startVal, midVal, t / fadeIn);
                t += Time.deltaTime;
                yield return null;
            }
            decalProjector.fadeFactor = midVal;
            //Fade out slow
            t = 0f;
            while (t < fadeOut && toggle)
            {
                decalProjector.fadeFactor = Mathf.Lerp(midVal, endVal, t / fadeOut);
                t += Time.deltaTime;
                yield return null;
            }
            decalProjector.fadeFactor = endVal;
            running = !running;

        }
    }
}
