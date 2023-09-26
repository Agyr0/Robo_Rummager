using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BilboardScaler : MonoBehaviour
{

    private Quaternion originalRot;
    private Vector3 originalScale = Vector3.zero;
    private PlayerController playerController;


    private float minDistance = 2f;
    private float maxDistance;
    private float minScale = 0f;
    private float maxScale = 4f;
    private float scale = 0;

    private Coroutine handleUI;


    private void Start()
    {
        originalRot = transform.GetChild(0).rotation;
        //transform.localScale = originalScale;
        playerController = GameManager.Instance.playerController;
        maxDistance = GetComponent<SphereCollider>().radius;
        transform.GetChild(0).localScale = new Vector3(minScale, minScale, minScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            handleUI = StartCoroutine(HandleUI());
            Debug.LogWarning("Started Coroutine");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (handleUI != null)
                StopCoroutine(handleUI);


            transform.GetChild(0).rotation = originalRot;
            Debug.LogWarning("Stopped Coroutine");

        }
    }

    private IEnumerator HandleUI()
    {
        while (true)
        {
            //Billboard to camera
            transform.GetChild(0).rotation = Camera.main.transform.rotation * originalRot;

            //Scale according to distance
            scale = Mathf.Lerp(Mathf.InverseLerp(minScale, maxScale, Vector3.Distance(transform.position, playerController.transform.position)),
                Mathf.InverseLerp(maxScale, minScale, Vector3.Distance(transform.position, playerController.transform.position)), 
                Mathf.InverseLerp(minDistance, maxDistance, Vector3.Distance(transform.position, playerController.transform.position)));
            
            transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
            yield return null;

        }

        


    }
}
