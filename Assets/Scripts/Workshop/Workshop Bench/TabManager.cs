using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agyr.Workshop
{
    public class TabManager : MonoBehaviour
    {
        [Space(10)]
        public TierController tier1Controller;
        public TierController tier2Controller;
        public TierController tier3Controller;


        private void OnEnable()
        {
            //EventBus.Subscribe(EventType.TIER_1_ROBOTS, EnableTier1);
            //EventBus.Subscribe(EventType.TIER_2_ROBOTS, EnableTier2);
            //EventBus.Subscribe(EventType.TIER_3_ROBOTS, EnableTier3);

            tier1Controller.AssignData();

        }

        #region Enable Tabs
        public void EnableTier1() => tier1Controller.EnableTabs();
        public void EnableTier2() => tier2Controller.EnableTabs();
        public void EnableTier3() => tier3Controller.EnableTabs();

        #endregion


        #region Select Robots
        public void SelectTier1Tab(Button button) => tier1Controller.TabSelected(button);

        #endregion
    }



    [System.Serializable]
    public class TierController
    {

        [SerializeField]
        private GameObject tabGroup;

        [SerializeField]
        #if UNITY_EDITOR
        [ArrayElementTitle("elementName")]
        #endif
        private List<TabButtonHeader> myTabs = new List<TabButtonHeader>();

        private WorkshopStorage workshopStorage;

        [SerializeField]
        private Color inactiveColor;
        [SerializeField]
        private Color activeColor;
        [SerializeField]
        private Color lockedColor;


        public void AssignData()
        {
            workshopStorage = WorkshopManager.Instance.WorkshopStorage;

            for (int i = 0; i < myTabs.Count; i++)
            {
                myTabs[i].InitializeMe(workshopStorage);
            }
        }
        public void EnableTabs()
        {
            //Enable all tabs
            tabGroup.SetActive(true);

            //Check for locked
            for (int i = 0; i < myTabs.Count; i++)
            {
                if(i == 0)
                {
                    myTabs[i].backgroundImage.color = activeColor;
                    myTabs[i].myContent.SetActive(true);
                }

                myTabs[i].myContent.SetActive(false);

                if (!myTabs[i].CheckUnlock())
                {
                    myTabs[i].buttonImage.color = lockedColor;
                }    
            }
        }

        public void TabSelected(Button button)
        {
            for (int i = 0; i < myTabs.Count; i++)
            {
                if (myTabs[i].myButton == button)
                {
                    myTabs[i].backgroundImage.color = activeColor;
                    myTabs[i].myContent.SetActive(true);
                }
                else
                {
                    myTabs[i].backgroundImage.color = inactiveColor;
                    myTabs[i].myContent.SetActive(false);
                }
            }
        }
    }



    [System.Serializable]
    public class TabButtonHeader
    {
        public Button myButton;
        public GameObject myContent;
        public Image backgroundImage;
        public Image buttonImage;
        public bool isLocked = true;
        [Space(10)]
        public string elementName = "ChangeMe";

        [Space(10)]
        public RobotCost myCost;

        public RoboIcons myIcons;

        public void InitializeMe(WorkshopStorage workshopStorage)
        {
            AssignData(workshopStorage);
            CheckUnlock();
        }

        private void AssignData(WorkshopStorage workshopStorage)
        {
            myIcons.creditIcon.sprite = workshopStorage.creditIcon;
            myIcons.motherBoardIcon.sprite = workshopStorage.motherBoardIcon;
            myIcons.wireIcon.sprite = workshopStorage.wireIcon;
            myIcons.oilIcon.sprite = workshopStorage.oilIcon;
            myIcons.metalScrapIcon.sprite = workshopStorage.metalScrapIcon;
            myIcons.sensorIcon.sprite = workshopStorage.sensorIcon;
            myIcons.zCrystalIcon.sprite = workshopStorage.zCrystalIcon;
            myIcons.radioactiveWasteIcon.sprite = workshopStorage.radioactiveWasteIcon;
            myIcons.blackMatterIcon.sprite = workshopStorage.blackMatterIcon;

            myIcons.creditCost.text = myCost.creditCost.ToString();
            myIcons.motherBoardCost.text = myCost.motherBoardCost.ToString();
            myIcons.wireCost.text = myCost.wireCost.ToString();
            myIcons.oilCost.text = myCost.oilCost.ToString();
            myIcons.metalScrapCost.text = myCost.metalScrapCost.ToString();
            myIcons.sensorCost.text = myCost.sensorCost.ToString();
            myIcons.zCrystalCost.text = myCost.zCrystalCost.ToString();
            myIcons.radioactiveWasteCost.text = myCost.radioactiveWasteCost.ToString();
            myIcons.blackMatterCost.text = myCost.blackMatterCost.ToString();
        }

        public bool CheckUnlock()
        {
            if(isLocked)
            {
                myButton.interactable = false;
                return false;
            }

            myButton.interactable = true;
            return true;
        }
        
    }



    [System.Serializable]
    public struct RoboIcons
    {
        public Image creditIcon;
        public Text creditCost;
        [Space(10)]
        public Image motherBoardIcon;
        public Text motherBoardCost;
        [Space(10)]
        public Image wireIcon;
        public Text wireCost;
        [Space(10)]
        public Image oilIcon;
        public Text oilCost;
        [Space(10)]
        public Image metalScrapIcon;
        public Text metalScrapCost;
        [Space(10)]
        public Image sensorIcon;
        public Text sensorCost;
        [Space(10)]
        public Image zCrystalIcon;
        public Text zCrystalCost;
        [Space(10)]
        public Image radioactiveWasteIcon;
        public Text radioactiveWasteCost;
        [Space(10)]
        public Image blackMatterIcon;
        public Text blackMatterCost;

    }

    [System.Serializable]
    public struct RobotCost
    {
        public int creditCost;
        public int motherBoardCost;
        public int wireCost;
        public int oilCost;
        public int metalScrapCost;
        public int sensorCost;
        public int zCrystalCost;
        public int radioactiveWasteCost;
        public int blackMatterCost;
    }

}