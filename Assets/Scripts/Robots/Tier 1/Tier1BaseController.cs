using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier1BaseController : MonoBehaviour, IInteractable
{
    private bool pickedUp = false;

    public void HandleInteract()
    {
        if(!pickedUp)
        {
            pickedUp = true;
            transform.position = GameManager.Instance.playerController.handTransform.position;
            transform.parent = GameManager.Instance.playerController.handTransform;
            StartCoroutine(LerpRotation(-GameManager.Instance.playerController.handTransform.forward));
        }
        else
        {
            pickedUp = false;
            transform.parent = null;
            StartCoroutine(LerpRotation(GameManager.Instance.playerController.handTransform.forward));

        }
    }

    private IEnumerator LerpRotation(Vector3 directionToFace)
    {
        float duration = 5f;
        float time = 0;
        Vector3 startPos = transform.position;
        while(time < duration)
        {
            transform.forward = Vector3.Lerp(startPos, directionToFace, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = directionToFace;
    }
}
