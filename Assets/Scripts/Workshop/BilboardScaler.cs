using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BilboardScaler : MonoBehaviour
{



    private Quaternion originalRot;
    private PlayerController playerController;


    private float minDistance = 0.5f;
    private float maxDistance = 3f;
    private float minScale = 0.5f;
    private float maxScale = 4f;
    private float scale = 0;


    private void Start()
    {
        originalRot = transform.rotation;
        playerController = GameManager.Instance.playerController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //StartCoroutine(HandleUI());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //StopCoroutine(HandleUI());
        }
    }

    private IEnumerator HandleUI()
    {
        while(true)
        {
            //Billboard to camera
            transform.rotation = Camera.main.transform.rotation * originalRot;

            //Scale according to distance
            scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDistance, maxDistance, minDistance));
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
