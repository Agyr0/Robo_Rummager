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
    public bool hasPlayedContractBoardTutorial = false;
    public bool hasPlayedContractAcceptedTutorial = false;
    public bool hasPlayedScannerGogglesTutorial = false;
    public bool hasPlayedLootingTutorial = false;
    public bool hasPlayedDropOffTutorial = false;
    public bool hasPlayedDropOffReachedTutorial = false;
    public bool hasPlayedSellingTutorial = false;

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
        EventBus.Subscribe<GameObject>(EventType.INVENTORY_ADDITEM, DropOffTutorialMessages);
        EventBus.Subscribe(EventType.DROPOFF_TUTORIAL, DropOffReachedTutorialMessages);
        EventBus.Subscribe(EventType.SELLING_TUTORIAL, SellingTutorialMessages);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.GAME_START, StartTutorialMessage);
        EventBus.Unsubscribe(EventType.CONTRACTS_TUTORIALS, ContractBoardTutorialMessages);
        EventBus.Unsubscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, ContractAcceptedTutorialMessages);
        EventBus.Unsubscribe(EventType.TOGGLE_SCANNER, ScannerGogglesTutorialMessages);
        EventBus.Unsubscribe(EventType.LOOTABLE_TUTORIAL, LootingTutorialMessages);
        EventBus.Unsubscribe<GameObject>(EventType.INVENTORY_ADDITEM, DropOffTutorialMessages);
        EventBus.Unsubscribe(EventType.SELLING_TUTORIAL, SellingTutorialMessages);
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

    // Drop Off Tutorial
    public void DropOffTutorialMessages(GameObject gameobject)
    {
        if (hasPlayedLootingTutorial && !hasPlayedDropOffTutorial)
        {
            tutorialContainer.SetActive(true);
            StartCoroutine(WriteDropOffTutorialMessages());
            hasPlayedDropOffTutorial = true;
        }
    }

    // Drop Off Reached Tutorial
    public void DropOffReachedTutorialMessages()
    {
        if (hasPlayedDropOffTutorial)
        {
            tutorialContainer.SetActive(true);
            StartCoroutine(WriteDropOffReachedTutorialMessages());
        }
    }

    // Selling Tutorial
    public void SellingTutorialMessages()
    {
        if (hasPlayedSellingTutorial)
        {
            tutorialContainer.SetActive(true);
            StartCoroutine(WriteSellingTutorialMessages());
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
        tutorialObjectiveMarker.SetActive(false);

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

        tutorialContainer.SetActive(false);
        hasPlayedContractBoardTutorial = true;
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
        yield return new WaitForSeconds(2);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipHappy;

        // Use Scanner Goggle Message
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
        chipFace.sprite = chipNeutral;

        tutorialContainer.SetActive(false);
        hasPlayedContractAcceptedTutorial = true;
    }

    // WRITE SCANNER GOGGLES TUTORIAL
    IEnumerator WriteScannerGogglesTutorialMessages()
    {
        // Scanner Goggles Message
        tutorialWaypoint.waypointTarget = tutorialWaypoint.tutorialLootableObject;
        tutorialObjectiveMarker.SetActive(true);

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

        tutorialContainer.SetActive(false);
        hasPlayedScannerGogglesTutorial = true;
    }

    // WRITE LOOTING TUTORIAL
    IEnumerator WriteLootingTutorialMessages()
    {
        // Looting Message
        tutorialWaypoint.waypointTarget = tutorialWaypoint.dropOffBin;
        tutorialObjectiveMarker.SetActive(false);

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
        tutorialContainer.SetActive(false);
        chipFace.sprite = chipHappy;

        tutorialContainer.SetActive(false);
        hasPlayedLootingTutorial = true;
    }

    // WRITE DROP OFF TUTORIAL
    IEnumerator WriteDropOffTutorialMessages()
    {
        // Drop Off
        tutorialObjectiveMarker.SetActive(true);

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
        chipFace.sprite = chipNeutral;

        tutorialContainer.SetActive(false);
        hasPlayedDropOffTutorial = true;
    }

    // WRITE DROP OFF REACHED TUTORIAL
    IEnumerator WriteDropOffReachedTutorialMessages()
    {
        // Drop Off Reached
        tutorialObjectiveMarker.SetActive(false);

        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(4);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipHappy;

        hasPlayedDropOffReachedTutorial = true;

        // Check Contract
        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(4);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipNeutral;

        // Show them Selling Teleporter
        tutorialWaypoint.waypointTarget = tutorialWaypoint.selling;
        tutorialObjectiveMarker.SetActive(true);

        StartCoroutine(OpenTutorialBox(true));
        foreach (char character in Messages[Index].ToCharArray())
        {
            tutorialText.text += character;
            yield return new WaitForSeconds(messageSpeed);
        }
        yield return new WaitForSeconds(4);
        tutorialText.text = null;
        Index++;
        StartCoroutine(OpenTutorialBox(false));
        yield return new WaitForSeconds(scaleTime);
        chipFace.sprite = chipHappy;

        tutorialContainer.SetActive(false);
    }

    // WRITE SELLING TUTORIAL
    IEnumerator WriteSellingTutorialMessages()
    {
        // Selling Bin
        tutorialObjectiveMarker.SetActive(false);

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
        chipFace.sprite = chipNeutral;
    }
}
