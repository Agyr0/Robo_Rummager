using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private bool hasPlayedContractAcceptedTutorial = false;
    public bool hasPlayedScannerGogglesTutorial = false;
    private bool hasPlayedLootingTutorial = false;

    [SerializeField] private float messageSpeed;

    [SerializeField] private float scaleTime;
    [SerializeField] private float scaleAmount;

    private Vector3 tutorialContainerStartScale;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.GAME_START, StartTutorialMessage);
        EventBus.Subscribe(EventType.CONTRACTS_TUTORIALS, ContractBoardTutorialMessages);
        EventBus.Subscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, ContractAcceptedTutorialMessages);
        EventBus.Subscribe(EventType.TOGGLE_SCANNER, ScannerGogglesTutorialMessages);
        EventBus.Subscribe(EventType.LOOTABLE_TUTORIAL, LootingTutorialMessages);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.GAME_START, StartTutorialMessage);
        EventBus.Unsubscribe(EventType.CONTRACTS_TUTORIALS, ContractBoardTutorialMessages);
        EventBus.Unsubscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, ContractAcceptedTutorialMessages);
        EventBus.Unsubscribe(EventType.TOGGLE_SCANNER, ScannerGogglesTutorialMessages);
        EventBus.Unsubscribe(EventType.LOOTABLE_TUTORIAL, LootingTutorialMessages);
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

    // Contract Accepeted Tutorial
    public void ContractAcceptedTutorialMessages(GameObject gameobject)
    {
        if (hasPlayedContractBoardTutorial)
        {
            tutorialContainer.SetActive(true);
            StartCoroutine(WriteContractAcceptedTutorialMessages());
        }
    }

    // Scanner Goggles Tutorial
    public void ScannerGogglesTutorialMessages()
    {
        if (hasPlayedContractAcceptedTutorial && !hasPlayedScannerGogglesTutorial)
        {
            tutorialContainer.SetActive(true);
            StartCoroutine(WriteScannerGogglesTutorialMessages());
        }
    }

    // Looting Tutorial
    public void LootingTutorialMessages()
    {
        if (hasPlayedScannerGogglesTutorial)
        {
            tutorialContainer.SetActive(true);
            StartCoroutine(WriteLootingTutorialMessages());
        }
    }

    //|||||||||||||||||COROUTINES||||||||||||||||| 
    //|||||||||||||||||COROUTINES|||||||||||||||||
    //|||||||||||||||||COROUTINES|||||||||||||||||

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
        // Hello Im Chip Text
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(1);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipNeutral;


        // Welcome to Workshop Text
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(2);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipHappy;


        // Head to Contract Board Text
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(1);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipNeutral;
        tutorialContainer.SetActive(false);
        tutorialObjectiveMarker.SetActive(true);
        hasPlayedStartTutorial = true;
    }

    // WRITE CONTRACT BOARD TUTORIAL
    IEnumerator WriteContractBoardTutorialMessages()
    {
        // Contract Board Description Text
        StartCoroutine(OpenTutorialBox(true));
        tutorialObjectiveMarker.SetActive(false);
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(2);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipHappy;


        // Single Contract Text
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(1);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipNeutral;
        hasPlayedContractBoardTutorial = true;
    }

    // WRITE CONTRACT ACCEPTED TUTORIAL
    IEnumerator WriteContractAcceptedTutorialMessages()
    {
        // Contract accepted message
        tutorialWaypoint.waypointTarget = tutorialWaypoint.tutorialLootableObject;
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(1);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        tutorialContainer.SetActive(false);
        chipFace.sprite = chipNeutral;

        yield return new WaitForSeconds(0.5f);

        // Use Scanner Goggle Message
        tutorialContainer.SetActive(true);
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(1);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        tutorialContainer.SetActive(false);
        chipFace.sprite = chipHappy;
        hasPlayedContractAcceptedTutorial = true;
    }

    // WRITE SCANNER GOGGLES TUTORIAL
    IEnumerator WriteScannerGogglesTutorialMessages()
    {
        // Scanner Goggles Message
        StartCoroutine(OpenTutorialBox(true));
        tutorialObjectiveMarker.SetActive(true);
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(1);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        tutorialContainer.SetActive(false);
        chipFace.sprite = chipNeutral;
        hasPlayedScannerGogglesTutorial = true;
    }

    // WRITE LOOTING TUTORIAL
    IEnumerator WriteLootingTutorialMessages()
    {
        // Looting Message
        StartCoroutine(OpenTutorialBox(true));
        tutorialObjectiveMarker.SetActive(true);
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(1);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        tutorialContainer.SetActive(false);
        chipFace.sprite = chipHappy;
        hasPlayedLootingTutorial = true;
    }
}
