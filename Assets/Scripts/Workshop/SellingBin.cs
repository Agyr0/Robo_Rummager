using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class SellingBin : MonoBehaviour
{
    private Player_Contract_Manager player_Contract_Manager;

    [SerializeField]
    private VisualEffect soldEffect;
    [SerializeField]
    private GameObject errorObj;
    [SerializeField]
    private float errorMessageDuration = 4f;

    private float scale = 0;
    private float timeToLerp = 0.25f;

    private bool showError = false;


    private Coroutine scaleErrorCoroutine = null;

    private void Start()
    {
        player_Contract_Manager = GameManager.Instance.inventoryManager.player_Contract_Manager;
    }


    private void OnTriggerEnter(Collider other)
    {
        BaseRobotPetController controller = other.GetComponent<BaseRobotPetController>();
        if (controller != null)
        {
            if (controller.CheckContract(player_Contract_Manager))
            {
                //controller.HandleInteract();
                controller.animator.Play("Dissolve");
                soldEffect.Play();
            }
            else
            {
                showError = true;
                if(scaleErrorCoroutine == null)
                    scaleErrorCoroutine = StartCoroutine(ScaleErrorMessage());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BaseRobotPetController controller = other.GetComponent<BaseRobotPetController>();
        if (controller != null)
        {
            if (!controller.CheckContract(player_Contract_Manager))
            {
                showError = false;
                if(scaleErrorCoroutine != null)
                    scaleErrorCoroutine = StartCoroutine(ScaleErrorMessage());

                scaleErrorCoroutine = null;
            }
        }

    }
    private IEnumerator ScaleErrorMessage()
    {
        float time = 0;
        if (showError)
        {
            errorObj.SetActive(true);

            while (time < timeToLerp)
            {
                scale = Mathf.Lerp(scale, 1, time / timeToLerp);

                errorObj.transform.localScale = new Vector3(scale, scale, scale);
                time += Time.deltaTime;

                yield return null;
            }
        }
        else
        {
            time = 0;
            while (time < (timeToLerp / 2))
            {
                scale = Mathf.Lerp(scale, 0, time / (timeToLerp / 2));

                errorObj.transform.localScale = new Vector3(scale, scale, scale);
                time += Time.deltaTime;

                yield return null;
            }
            errorObj.SetActive(false);

        }
        yield return new WaitForSeconds(2f);
    }


}
