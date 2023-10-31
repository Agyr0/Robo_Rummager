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

    public bool hasPlayedStartTutorial = false;
    private bool hasPlayedContractBoardTutorial = false;
    private bool hasPlayedContractOpenedTutorial = false;
    private bool hasPlayedContractAcceptedTutorial = false;

    [SerializeField] private float messageSpeed;

    [SerializeField] private float scaleTime;
    [SerializeField] private float scaleAmount;

    private Vector3 tutorialContainerStartScale;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.GAME_START, StartTutorialMessage);
        EventBus.Subscribe(EventType.CONTRACTS_TUTORIALS, ContractBoardTutorialMessages);
        EventBus.Subscribe(EventType.CONTRACTS_OPENED_TUTORIALS, ContractOpenedTutorialMessages);
        EventBus.Subscribe(EventType.PLAYER_CONTRACTUPDATE, ContractAcceptedTutorialMessages);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.GAME_START, StartTutorialMessage);
        EventBus.Unsubscribe(EventType.CONTRACTS_TUTORIALS, ContractBoardTutorialMessages);
        EventBus.Unsubscribe(EventType.CONTRACTS_OPENED_TUTORIALS, ContractOpenedTutorialMessages);
        EventBus.Unsubscribe(EventType.PLAYER_CONTRACTUPDATE, ContractAcceptedTutorialMessages);
    }

    private void Start()
    {
        tutorialContainerStartScale = tutorialContainer.transform.localScale;
    }

    // Start of Game Tutorial
    private void StartTutorialMessage()
    {
        tutorialContainer.SetActive(true);
        StartCoroutine(WriteStartTutorialMessages());
    }

    // Contract Board Tutorial
    public void ContractBoardTutorialMessages()
    {
        if (hasPlayedStartTutorial)
        {
            tutorialContainer.SetActive(true);
            StartCoroutine(WriteContractBoardTutorialMessages());
        }
    }

    // Contract Opened Tutorial
    public void ContractOpenedTutorialMessages()
    {
        if (hasPlayedContractBoardTutorial)
        {
            tutorialContainer.SetActive(true);
            StartCoroutine(WriteContractOpenedTutorialMessages());
        }
    }

    // Contract Accepeted Tutorial
    public void ContractAcceptedTutorialMessages()
    {
        if (hasPlayedContractOpenedTutorial)
        {
            tutorialContainer.SetActive(true);
            StartCoroutine(WriteContractAcceptedTutorialMessages());
        }
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

    // WRITE START OF GAME TUTORIAL
    IEnumerator WriteStartTutorialMessages()
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
        hasPlayedStartTutorial = true;
    }

    // WRITE CONTRACT BOARD TUTORIAL
    IEnumerator WriteContractBoardTutorialMessages()
    {
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
        tutorialContainer.SetActive(false);
        chipFace.sprite = chipNeutral;
        hasPlayedContractBoardTutorial = true;
    }

    // WRITE CONTRACT OPENED TUTORIAL
    IEnumerator WriteContractOpenedTutorialMessages()
    {
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
        hasPlayedContractOpenedTutorial = true;
    }

    // WRITE CONTRACT ACCEPTED TUTORIAL
    IEnumerator WriteContractAcceptedTutorialMessages()
    {
        // Contract accepted message
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
        chipFace.sprite = chipNeutral;

        yield return new WaitForSeconds(3);

        // Use Scanner Goggle Message
        tutorialContainer.SetActive(true);
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
        hasPlayedContractAcceptedTutorial = true;
    }
}
