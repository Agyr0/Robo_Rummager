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


    [SerializeField]
    private float minDistance = 2f;
    private float maxDistance = 0;
    private float minScale = 0f;
    [SerializeField]
    private float maxScale = 4f;
    private float scale = 0;

    [SerializeField]
    private bool isBillboard = true;

    [SerializeField]
    private bool lockX, lockY, lockZ = false;


    private Coroutine handleUI;


    private void Start()
    {
        originalRot = transform.GetChild(0).localRotation;
        //transform.localScale = originalScale;
        playerController = GameManager.Instance.playerController;
        if(GetComponent<SphereCollider>() != null)
            maxDistance = GetComponent<SphereCollider>().radius;
        transform.GetChild(0).localScale = new Vector3(minScale, minScale, minScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            handleUI = StartCoroutine(HandleUI());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (handleUI != null)
                StopCoroutine(handleUI);


            transform.GetChild(0).localRotation = originalRot;

        }
    }

    public IEnumerator HandleUI()
    {
        while (true)
        {
                //Billboard to camera
            if(isBillboard)
                transform.GetChild(0).localRotation = Camera.main.transform.rotation * originalRot;

            //Scale according to distance
            scale = Mathf.Lerp(Mathf.InverseLerp(minScale, maxScale, Vector3.Distance(transform.position, playerController.transform.position)),
                Mathf.InverseLerp(maxScale, minScale, Vector3.Distance(transform.position, playerController.transform.position)), 
                Mathf.InverseLerp(minDistance, maxDistance, Vector3.Distance(transform.position, playerController.transform.position)));
            
            transform.GetChild(0).localScale = new Vector3(scale, scale, scale);

            if (lockX)
                transform.GetChild(0).localRotation = new Quaternion(0, transform.GetChild(0).localRotation.y, 
                    transform.GetChild(0).localRotation.z, transform.GetChild(0).localRotation.w);
            if (lockY)
                transform.GetChild(0).localRotation = new Quaternion(transform.GetChild(0).localRotation.x, 0,
                    transform.GetChild(0).localRotation.z, transform.GetChild(0).localRotation.w);
            if (lockZ)
                transform.GetChild(0).localRotation = new Quaternion(transform.GetChild(0).localRotation.x, transform.GetChild(0).localRotation.y,
                    0, transform.GetChild(0).localRotation.w);
            yield return null;

        }

        


    }
}
