using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agyr.Workshop
{
    public class TabManager : MonoBehaviour
    {

        [SerializeField]
        private Color inactiveColor;
        [SerializeField]
        private Color activeColor;
        [SerializeField]
        private Color lockedColor;

        [Space(10)]
        public Tier1Controller tier1Controller;
        public Tier2Controller tier2Controller;
        public Tier3Controller tier3Controller;


        private void OnEnable()
        {
            EventBus.Subscribe(EventType.TIER_1_ROBOTS, EnableTier1);
            EventBus.Subscribe(EventType.TIER_2_ROBOTS, EnableTier2);
            EventBus.Subscribe(EventType.TIER_3_ROBOTS, EnableTier3);
        }

        #region Enable Tabs
        public void EnableTier1() => tier1Controller.EnableTabs();
        public void EnableTier2() => tier2Controller.EnableTabs();
        public void EnableTier3() => tier3Controller.EnableTabs();

        #endregion


        #region Select Robots


        #endregion
    }



    [System.Serializable]
    public class Tier1Controller
    {

        [SerializeField]
        private GameObject tabGroup;

        [SerializeField]
        #if UNITY_EDITOR
        [ArrayElementTitle("elementName")]
        #endif
        private List<TabButtonHeader> myTabs = new List<TabButtonHeader>();

        public void EnableTabs()
        {

        }
    }


    [System.Serializable]
    public class Tier2Controller
    {
        [SerializeField]
        private GameObject tabGroup;

        public void EnableTabs()
        {

        }
    }


    [System.Serializable]
    public class Tier3Controller
    {
        [SerializeField]
        private GameObject tabGroup;

        public void EnableTabs()
        {

        }
    }






    [System.Serializable]
    public class TabButtonHeader
    {
        public GameObject myContent;
        public Image backgroundImage;
        public Image buttonImage;
        [Space(10)]
        public string elementName = "ChangeMe";
    }

}