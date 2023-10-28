using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialContainer;
    public GameObject tutorialObjectiveMarker;
    public TutorialWaypoint tutorialWaypoint;
    public TMP_Text tutorialText;
    public Image chipFace;
    public Sprite chipHappy;
    public Sprite chipNeutral;

    public string[] Messages;
    private int Index = 0;

    [SerializeField] private float messageSpeed;

    [SerializeField] private float scaleTime = 0.5f;
    [SerializeField] private float scaleAmount = 100f;

    private Vector3 tutorialContainerStartScale;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.GAME_START, FirstTutorialMessages);
        EventBus.Subscribe(EventType.CONTRACTS_TUTORIALS, ContractsTutorialMessages);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.GAME_START, FirstTutorialMessages);
        EventBus.Subscribe(EventType.CONTRACTS_TUTORIALS, ContractsTutorialMessages);
    }

    private void Start()
    {
        tutorialContainerStartScale = tutorialContainer.transform.localScale;
    }

    private void FirstTutorialMessages()
    {
        tutorialContainer.SetActive(true);
        StartCoroutine(WriteFirstTutorialMessages());
    }

    public void ContractsTutorialMessages()
    {
        tutorialContainer.SetActive(true);
        StartCoroutine(WriteContractsTutorialMessages());
    }

    // OPEN / CLOSE TUTORIAL TEXT BOX
    IEnumerator OpenTutorialBox(bool startingAnimation)
    {
        Vector3 endScale;

        float elapsedTime = 0;
        while (elapsedTime < scaleTime)
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

            Vector3 lerpedScale = Vector3.Lerp(tutorialContainer.transform.localScale, endScale, (elapsedTime / scaleTime));

            tutorialContainer.transform.localScale = lerpedScale;

            yield return null;
        }
    }

    // START OF GAME TUTORIALS
    IEnumerator WriteFirstTutorialMessages()
    {
        // Open first dialogue box
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(3);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipNeutral;


        // Open second dialogue box
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(3);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipHappy;


        // Open third dialogue box
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(3);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        tutorialContainer.SetActive(false);
        tutorialObjectiveMarker.SetActive(true);
    }

    // CONTRACT TUTORIALS
    IEnumerator WriteContractsTutorialMessages()
    {
        // Open first dialogue box
        StartCoroutine(OpenTutorialBox(true));
        tutorialObjectiveMarker.SetActive(false);
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(3);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipNeutral;

        // Open second dialogue box
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(3);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        tutorialContainer.SetActive(false);
        chipFace.sprite = chipHappy;
    }
}
