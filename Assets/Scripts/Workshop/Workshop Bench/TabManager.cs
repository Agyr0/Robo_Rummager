using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Agyr.CustomAttributes;

namespace Agyr.Workshop
{
    public class TabManager : MonoBehaviour
    {
        [Space(10)]
        public TierController tier1Controller;
        public TierController tier2Controller;
        public TierController tier3Controller;

        [SerializeField]
        private Transform prefabSpawnPoint;
        public Transform robotParent;

        private GameObject hologramInstance;

        private void OnEnable()
        {
            //EventBus.Subscribe(EventType.TIER_1_ROBOTS, EnableTier1);
            //EventBus.Subscribe(EventType.TIER_2_ROBOTS, EnableTier2);
            EventBus.Subscribe<TierController>(EventType.SPAWN_HOLOGRAM, SpawnRobotHologram);
            EventBus.Subscribe(EventType.ROBOT_TAKEN_OFF_WORKBENCH, ResetTab);
            EventBus.Subscribe(EventType.ROBOT_BUILT, RobotBuilt);

            tier1Controller.AssignData();

        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<TierController>(EventType.SPAWN_HOLOGRAM, SpawnRobotHologram);
            EventBus.Unsubscribe(EventType.ROBOT_TAKEN_OFF_WORKBENCH, ResetTab);
            EventBus.Unsubscribe(EventType.ROBOT_BUILT, RobotBuilt);
        }

        #region Enable Tabs
        public void EnableTier1() => tier1Controller.EnableTabs();
        public void EnableTier2() => tier2Controller.EnableTabs();
        public void EnableTier3() => tier3Controller.EnableTabs();

        #endregion


        #region Select Tabs
        public void SelectTier1Tab(Button button) => tier1Controller.TabSelected(button);

        #endregion

        #region Select Robots
        public void SelectTier1Robot(Button button) => tier1Controller.SelectRobot(button, robotParent, hologramInstance);
        public void CancelTier1Robot(Button button) => tier1Controller.CancelRobot(button, hologramInstance);

        #endregion

        private void RobotBuilt() => FindActiveTab().ToggleCancelButton();

        #region Reset Tabs
        private void ResetTab() => FindActiveTab().ResetTab(WorkshopManager.Instance.WorkshopStorage);

        #endregion

        private void SpawnRobotHologram(TierController controller)
        {
            hologramInstance = Instantiate(controller.hologramPrefab, prefabSpawnPoint.position, prefabSpawnPoint.rotation, robotParent);
            hologramInstance.GetComponent<PetBuildingController>().myTab = controller.selectedTab;
            

            Debug.Log("Spawned hologram");
        }

        public TabButtonHeader FindActiveTab()
        {
            if (tier1Controller.tabGroup.activeInHierarchy)
            {
                for (int i = 0; i < tier1Controller.myTabs.Count; i++)
                {
                    if (tier1Controller.myTabs[i].backgroundImage.color == tier1Controller.activeColor)
                        return tier1Controller.myTabs[i];

                }
            }
            else if (tier2Controller.tabGroup.activeInHierarchy)
            {
                for (int i = 0; i < tier2Controller.myTabs.Count; i++)
                {
                    if (tier2Controller.myTabs[i].backgroundImage.color == tier2Controller.activeColor)
                        return tier2Controller.myTabs[i];

                }
            }
            else if (tier3Controller.tabGroup.activeInHierarchy)
            {
                for (int i = 0; i < tier3Controller.myTabs.Count; i++)
                {
                    if (tier3Controller.myTabs[i].backgroundImage.color == tier3Controller.activeColor)
                        return tier3Controller.myTabs[i];

                }
            }

            return null;

        }

        public void RefreshWorkbenchUI() => FindActiveTab().CheckResourceCount(WorkshopManager.Instance.WorkshopStorage);
    }



    [System.Serializable]
    public class TierController
    {


        public GameObject tabGroup;
        [HideInInspector]
        public GameObject hologramPrefab;
        [HideInInspector]
        public TabButtonHeader selectedTab;

#if UNITY_EDITOR
        [ArrayElementTitle("elementName")]
#endif
        public List<TabButtonHeader> myTabs = new List<TabButtonHeader>();

        private WorkshopStorage workshopStorage;

        public Color inactiveColor;
        public Color activeColor;
        public Color lockedColor;



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
                myTabs[i].myContent.SetActive(false);
                myTabs[i].backgroundImage.color = inactiveColor;

                if (i == 0)
                {
                    myTabs[i].backgroundImage.color = activeColor;
                    myTabs[i].myContent.SetActive(true);
                }


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
                    myTabs[i].CheckResourceCount(workshopStorage);
                    myTabs[i].myContent.SetActive(true);
                }
                else
                {
                    myTabs[i].backgroundImage.color = inactiveColor;
                    myTabs[i].CheckResourceCount(workshopStorage);
                    myTabs[i].myContent.SetActive(false);
                }
            }
        }

        public void SelectRobot(Button button, Transform robotParent, GameObject hologram)
        {
            for (int i = 0; i < myTabs.Count; i++)
            {
                if (myTabs[i].selectButton == button)
                {
                    if (myTabs[i].CheckResourceCount(workshopStorage) && robotParent.childCount == 0)
                    {
                        myTabs[i].BuyRobot(workshopStorage);
                        hologramPrefab = myTabs[i].hologramPrefab;
                        selectedTab = myTabs[i];
                        EventBus.Publish(EventType.SPAWN_HOLOGRAM, this);
                        myTabs[i].selectButton.interactable = false;
                        myTabs[i].ButtonText.Value = "Already Building";
                        myTabs[i].hasPurchased = true;
                        myTabs[i].ToggleCancelButton();
                        
                    }
                    return;
                }
            }
        }

        


        public void CancelRobot(Button button, GameObject hologram)
        {
            for (int i = 0; i < myTabs.Count; i++)
            {
                if (myTabs[i].myTab.cancelButton == button)
                {
                    myTabs[i].RefundResources(workshopStorage);
                    myTabs[i].ToggleCancelButton();

                }
            }


            PetBuildingController petController = hologram.GetComponent<PetBuildingController>();

            petController.ResetRobot();
        }
        




    }


    [System.Serializable]
    public class TabButtonHeader
    {
        public TabHeader myTab;

        public Button myButton
        {
            get { return myTab.myButton; }
            set { myTab.myButton = value; }
        }
        public Image buttonImage
        {
            get { return myTab.buttonImage; }
            set { myTab.buttonImage = value; }
        }
        public GameObject myContent
        {
            get { return myTab.myContent; }
            set { myTab.myContent = value; }
        }
        public Button selectButton
        {
            get { return myTab.selectButton; }
            set { myTab.selectButton = value; }
        }
        public Image backgroundImage
        {
            get { return myTab.backgroundImage; }
            set { myTab.backgroundImage = value; }
        }
        public GameObject hologramPrefab
        {
            get { return myTab.hologramPrefab; }
            set { myTab.hologramPrefab = value; }
        }



        public bool isLocked = true;
        public bool hasPurchased = false;
        [Space(10)]
        public string elementName = "ChangeMe";
        private bool cancelActive = false;

        [HideInInspector]
        public Property<string> ButtonText;

        public RobotCost myCost
        {
            get { return myTab.myCost; }
            set { myTab.myCost = value; }
        }

        public RoboIcons myIcons
        {
            get { return myTab.myIcons; }
            set { myTab.myIcons = value; }
        }

        public void InitializeMe(WorkshopStorage workshopStorage)
        {
            AssignData(workshopStorage);
            CheckUnlock();
        }

        private void AssignData(WorkshopStorage workshopStorage)
        {
            myButton = myTab.myButton;
            buttonImage = myTab.buttonImage;
            myContent = myTab.myContent;
            selectButton = myTab.selectButton;
            backgroundImage = myTab.backgroundImage;
            hologramPrefab = myTab.hologramPrefab;
            myCost = myTab.myCost;
            myIcons = myTab.myIcons;


            ButtonText.Value = "Not Enough Resources";

            myIcons.motherBoardIcon.sprite = workshopStorage.motherBoardIcon;
            myIcons.wireIcon.sprite = workshopStorage.wireIcon;
            myIcons.oilIcon.sprite = workshopStorage.oilIcon;
            myIcons.metalScrapIcon.sprite = workshopStorage.metalScrapIcon;
            myIcons.sensorIcon.sprite = workshopStorage.sensorIcon;
            myIcons.zCrystalIcon.sprite = workshopStorage.zCrystalIcon;
            myIcons.radioactiveWasteIcon.sprite = workshopStorage.radioactiveWasteIcon;
            myIcons.blackMatterIcon.sprite = workshopStorage.blackMatterIcon;

            myIcons.motherBoardCost.text = myCost.motherBoardCost.ToString();
            myIcons.wireCost.text = myCost.wireCost.ToString();
            myIcons.oilCost.text = myCost.oilCost.ToString();
            myIcons.metalScrapCost.text = myCost.metalScrapCost.ToString();
            myIcons.sensorCost.text = myCost.sensorCost.ToString();
            myIcons.zCrystalCost.text = myCost.zCrystalCost.ToString();
            myIcons.radioactiveWasteCost.text = myCost.radioactiveWasteCost.ToString();
            myIcons.blackMatterCost.text = myCost.blackMatterCost.ToString();

            RuntimeBindingExtensions.BindProperty(selectButton.gameObject.GetComponentInChildren<Text>(), ButtonText);

        }

        public bool CheckUnlock()
        {
            if (isLocked)
            {
                myButton.interactable = false;
                return false;
            }

            myButton.interactable = true;
            return true;
        }

        public bool CheckResourceCount(WorkshopStorage workshopStorage)
        {
            if (myCost.motherBoardCost <= workshopStorage.MotherBoardCount &&
                myCost.wireCost <= workshopStorage.WireCount &&
                myCost.oilCost <= workshopStorage.OilCount &&
                myCost.metalScrapCost <= workshopStorage.MetalScrapCount &&
                myCost.sensorCost <= workshopStorage.SensorCount &&
                myCost.zCrystalCost <= workshopStorage.ZCrystalCount &&
                myCost.radioactiveWasteCost <= workshopStorage.RadioactiveWasteCount &&
                myCost.blackMatterCost <= workshopStorage.BlackMatterCount && !hasPurchased)
            {
                ButtonText.Value = "Select";
                selectButton.interactable = true;
                return true;
            }
            else if (!hasPurchased)
            {
                selectButton.interactable = false;
                ButtonText.Value = "Not Enough Resources";
                return false;
            }
            return false;
        }

        public void ToggleCancelButton()
        {
            cancelActive = !cancelActive;
            myTab.cancelButton.gameObject.SetActive(cancelActive);
        }
        public void BuyRobot(WorkshopStorage workshopStorage)
        {
            workshopStorage.MotherBoardCount -= myCost.motherBoardCost;

            workshopStorage.WireCount -= myCost.wireCost;

            workshopStorage.OilCount -= myCost.oilCost;

            workshopStorage.MetalScrapCount -= myCost.metalScrapCost;

            workshopStorage.SensorCount -= myCost.sensorCost;

            workshopStorage.ZCrystalCount -= myCost.zCrystalCost;

            workshopStorage.RadioactiveWasteCount -= myCost.radioactiveWasteCost;

            workshopStorage.BlackMatterCount -= myCost.blackMatterCost;
        }
        public void RefundResources(WorkshopStorage workshopStorage)
        {
            workshopStorage.MotherBoardCount += myCost.motherBoardCost;
            workshopStorage.WireCount += myCost.wireCost;
            workshopStorage.OilCount += myCost.oilCost;
            workshopStorage.MetalScrapCount += myCost.metalScrapCost;
            workshopStorage.SensorCount += myCost.sensorCost;
            workshopStorage.ZCrystalCount += myCost.zCrystalCost;
            workshopStorage.RadioactiveWasteCount += myCost.radioactiveWasteCost;
            workshopStorage.BlackMatterCount += myCost.blackMatterCost;

            hasPurchased = false;
            selectButton.interactable = true;
            ButtonText.Value = "Select";
        }

        public void ResetTab(WorkshopStorage workshopStorage)
        {
            hasPurchased = false;
            CheckResourceCount(workshopStorage);
        }
    }



    [System.Serializable]
    public struct RoboIcons
    {
        public Image previewIcon;
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