using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class DetectionController : MonoBehaviour
{
    private VisualEffect scannerVFX;
    [SerializeField]
    private Transform target;
    public bool send = false;
    private float radius = 0f;
    private float VFXSize;
    private void OnEnable()
    {
        scannerVFX = GetComponent<VisualEffect>();
        EventBus.Subscribe(EventType.SEND_DETECTION_SPHERE, ToggleDetection);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.SEND_DETECTION_SPHERE, ToggleDetection);
    }


    private void ToggleDetection()
    {
       // VFXSize = scannerVFX.GetFloat("SizeMultiplier");
        
        send = !send;

        if(send)
            StartCoroutine(Detect());
        else
            StopCoroutine(Detect());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position, radius);
    }


    private IEnumerator Detect()
    {
        while (true && send)
        {
            float t = 0f;
            float lifetime = scannerVFX.GetFloat("Lifetime");
            float startSize = scannerVFX.GetFloat("StartSize");
            float rangeMultiplier = scannerVFX.GetFloat("RangeMultiplier");
            scannerVFX.SendEvent(VisualEffectAsset.PlayEventID);

            yield return new WaitForSeconds(scannerVFX.playRate + lifetime);

            while (t < lifetime && send)
            {
                radius = scannerVFX.GetAnimationCurve("SizeMultiplier").Evaluate(t) * startSize * rangeMultiplier;
                t += Time.deltaTime;
                Collider[] hits = Physics.OverlapSphere(target.position, radius);

                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].GetComponent<ItemGlowController>() != null)
                        hits[i].GetComponent<ItemGlowController>().PlayGlow();
                        


                }
                yield return null;
            }
            radius = 0;
        }
    }
}
