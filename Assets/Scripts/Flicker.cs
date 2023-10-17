using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    [SerializeField]
    private int _delay;

    private bool _isFading;

    private void OnEnable()
    {
        StartCoroutine(Flickering());
    }
    private void OnDisable() 
    { 
        StopAllCoroutines();
    }

    IEnumerator Flickering()
    {
        Color trt = this.GetComponent<TextMeshProUGUI>().color;
        yield return new WaitForSeconds(_delay);
        while (true) 
        {
            while (_isFading)
            {
                if (this.GetComponent<TextMeshProUGUI>().color.a <= .01f)
                {
                    _isFading = false;
                    Debug.Log(trt.a);
                }
                else
                {
                    trt.a -= .01f;
                    this.GetComponent<TextMeshProUGUI>().color = trt;
                }
                yield return new WaitForSeconds(.001f);
            }
            while (!_isFading)
            {
                if (this.GetComponent<TextMeshProUGUI>().color.a >= .98f)
                {
                    _isFading = true;
                    Debug.Log(trt.a);
                }
                else
                {
                    trt.a += .01f;
                    this.GetComponent<TextMeshProUGUI>().color = trt;
                }
                yield return new WaitForSeconds(.001f);
            }
            yield return new WaitForSeconds(.001f);
        }
    }
}
