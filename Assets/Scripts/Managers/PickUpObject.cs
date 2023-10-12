using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PickUpObject : MonoBehaviour, IInteractable
{
    private bool pickedUp = true;
    [SerializeField]
    private Vector3 offSet;
    private Rigidbody rb;



    public virtual void HandleInteract()
    {
        pickedUp = !pickedUp;

        if (!pickedUp)
        {
            transform.parent = GameManager.Instance.playerController.handTransform;
            transform.localPosition = new Vector3(GameManager.Instance.playerController.handTransform.localPosition.x * offSet.x,
                GameManager.Instance.playerController.handTransform.localPosition.y * offSet.y,
                GameManager.Instance.playerController.handTransform.localPosition.z * offSet.z);
            if (rb == null)
                rb = GetComponent<Rigidbody>();

            rb.isKinematic = true;

            StartCoroutine(LerpRotation(true));
        }
        else
        {
            transform.parent = null;
            if (rb == null)
                rb = GetComponent<Rigidbody>();

            rb.isKinematic = false;

            StartCoroutine(LerpRotation(false));

        }
    }

    private IEnumerator LerpRotation(bool towards)
    {
        float duration = 1f;
        float time = 0;
        Quaternion rotation = transform.rotation;
        Quaternion startPos = transform.rotation;
        if (towards)
            while (time < duration)
            {
                rotation =
                    Quaternion.LookRotation(-GameManager.Instance.playerController.handTransform.forward, GameManager.Instance.playerController.handTransform.up);
                transform.rotation = Quaternion.Lerp(startPos, rotation, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        else
        {
            rotation =
                Quaternion.LookRotation(GameManager.Instance.playerController.handTransform.forward, GameManager.Instance.playerController.handTransform.up);
            while (time < duration)
            {
                transform.rotation = Quaternion.Lerp(startPos, rotation, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        }
        transform.rotation = rotation;
    }
}
