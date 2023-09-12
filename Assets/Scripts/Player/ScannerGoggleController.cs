using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ScannerGoggleController : MonoBehaviour
{
    [SerializeField] private RectTransform barTransform;
    private RectTransform canvasRect;
    private float distance = 0f;
    [SerializeField] private float speed = 40f;
    private bool movingRight = true;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        canvasRect = gameObject.GetComponent<RectTransform>();
        distance = canvasRect.rect.width;
        barTransform.position = Vector3.zero;
        StartCoroutine(PingPongBar());

    }
    private void OnDisable()
    {
        StopCoroutine(PingPongBar());
    }

    private IEnumerator PingPongBar()
    {
        while (true)
        {
            Vector3 startPosition = barTransform.position;
            Vector3 targetPosition;

            if (movingRight)
            {
                targetPosition = new Vector3(distance, barTransform.position.y, barTransform.position.z);
            }
            else
            {
                targetPosition = new Vector3(-130, barTransform.position.y, barTransform.position.z);
            }

            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float startTime = Time.time;

            while (Time.time < startTime + (journeyLength / speed))
            {
                float distanceCovered = (Time.time - startTime) * speed;
                float journeyFraction = distanceCovered / journeyLength;
                barTransform.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);
                yield return null;
            }

            barTransform.position = targetPosition;
            movingRight = !movingRight;
            yield return new WaitForSeconds(.5f);
        }
    }
}
