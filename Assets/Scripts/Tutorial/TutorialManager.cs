using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text tutorialsText;
    [SerializeField] private float moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float scaleAmount = 1.1f;

    private Vector3 startScale;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.GAME_START, FirstLoadTutorial);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.GAME_START, FirstLoadTutorial);
    }

    private void FirstLoadTutorial()
    {
        Debug.Log("Call Worked");
        StartCoroutine(OpenTutorialBox(true));
    }

    IEnumerator OpenTutorialBox(bool startingAnimation)
    {
        Debug.Log("Entered Coroutine for Tutorial Animation");
        Vector3 endScale;

        float elapsedTime = 0;
        while(elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;

            if(startingAnimation)
            {
                endScale = startScale * scaleAmount;
            }

            else
            {
                endScale = startScale;
            }

            Vector3 lerpedScale = Vector3.Lerp(transform.position, endScale, (elapsedTime / moveTime));

            transform.localScale = lerpedScale;

            yield return null;
        }
    }
}
