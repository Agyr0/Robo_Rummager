using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialContainer;
    public TMP_Text tutorialText;
    public string[] Messages;
    private int Index = 0;
    [SerializeField] private float messageSpeed;

    [SerializeField] private float moveTime = 0.1f;
    [SerializeField] private float scaleAmount = 1.1f;

    private Vector3 tutorialContainerStartScale;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.GAME_START, FirstLoadTutorial);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.GAME_START, FirstLoadTutorial);
    }

    private void Start()
    {
        tutorialContainerStartScale = tutorialContainer.transform.localScale;
    }

    private void FirstLoadTutorial()
    {
        tutorialContainer.SetActive(true);
        StartCoroutine(OpenTutorialBox(true));
        StartCoroutine(WriteMessage());
    }

    IEnumerator OpenTutorialBox(bool startingAnimation)
    {
        Vector3 endScale;

        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;

            if (startingAnimation)
            {
                endScale = tutorialContainerStartScale * scaleAmount;
            }

            else
            {
                endScale = tutorialContainerStartScale;
            }

            Vector3 lerpedScale = Vector3.Lerp(tutorialContainer.transform.localScale, endScale, (elapsedTime / moveTime));

            tutorialContainer.transform.localScale = lerpedScale;

            yield return null;
        }
    }

    IEnumerator WriteMessage()
    {
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        Index++;
    }
}
