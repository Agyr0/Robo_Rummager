using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Upgrade_Robot : Upgrade_UI_Behavior
{
    
    [SerializeField]
    private int _robotToUnlock = 0;

    [SerializeField]
    private Robot_RecipeData _recipeDataContract;



    [SerializeField]
    private int _upgradeCurrentLevel = 0;
    [SerializeField]
    private int _upgradeMaxLevel = 1;
    [SerializeField]
    private int _upgradeCost = 5;
    [SerializeField]
    private string _upgradeDesc = "";

    private void Start()
    {
        UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);
        //_tabManager = GameObject.Find("WorkshopArea/Workbench").transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<TabManager>(); ;
    }

    public void Upgrade()
    {
        if (_upgradeCurrentLevel < _upgradeMaxLevel)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCost)
            {
                AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.effectAudio[6].myControllers[1]);
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCost;
                GameManager.Instance.inventoryManager.CreditText = WorkshopManager.Instance.WorkshopStorage.CreditCount.ToString();

                //Loop through robot tabs in workbench
                for (int i = 0; i < WorkshopBench.Instance.tabManager.tier1Controller.myTabs.Count; i++)
                {
                    //Find the correct one
                    if (WorkshopBench.Instance.tabManager.tier1Controller.myTabs[i].myRecipieData == _recipeDataContract)
                        //Unlock tab
                        WorkshopBench.Instance.tabManager.tier1Controller.myTabs[i].isLocked = false;
                }
                WorkshopBench.Instance.tabManager.tier1Controller.EnableTabs();


                ContractBoard_Manager.Instance.Robot_RecipeDataList.Add(_recipeDataContract);

                _upgradeCurrentLevel++;
                UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);
            }
        }
    }
}
