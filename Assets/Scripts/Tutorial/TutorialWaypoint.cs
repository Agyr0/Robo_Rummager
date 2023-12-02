using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialWaypoint : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public Image waypointImage;
    public Transform waypointTarget;
    public Transform playerTransform;
    public TMP_Text meters;
    public Vector3 offset;

    public Transform tutorialLootableObject;

    private bool hasTriggeredContractTutorial = false;
    private bool hasTriggeredLootingTutorial = false;

    private void Update()
    {
        if (!hasTriggeredContractTutorial)
        {
            float minX = waypointImage.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;

            float minY = waypointImage.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(waypointTarget.position + offset);

            if (Vector3.Dot((waypointTarget.position - playerTransform.position), playerTransform.forward) < 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            waypointImage.transform.position = pos;
            meters.text = ((int)Vector3.Distance(waypointTarget.position, playerTransform.position)).ToString() + "m";

            if (hasTriggeredContractTutorial == false)
            {
                if ((int)Vector3.Distance(waypointTarget.position, playerTransform.position) < 5 && tutorialManager.hasPlayedStartTutorial == true)
                {
                    EventBus.Publish(EventType.CONTRACTS_TUTORIALS);
                    hasTriggeredContractTutorial = true;
                }
            }
        }

        else if (!hasTriggeredLootingTutorial)
        {
            float minX = waypointImage.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;

            float minY = waypointImage.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(waypointTarget.position + offset);

            if (Vector3.Dot((waypointTarget.position - playerTransform.position), playerTransform.forward) < 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            waypointImage.transform.position = pos;
            meters.text = ((int)Vector3.Distance(waypointTarget.position, playerTransform.position)).ToString() + "m";

            if (hasTriggeredLootingTutorial == false)
            {
                if ((int)Vector3.Distance(waypointTarget.position, playerTransform.position) < 5 && tutorialManager.hasPlayedScannerGogglesTutorial == true)
                {
                    EventBus.Publish(EventType.LOOTABLE_TUTORIAL);
                    hasTriggeredLootingTutorial = true;
                }
            }
        }
    }
}
