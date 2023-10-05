using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRobotPetController : PickUpObject, IInteractable
{
    [SerializeField]
    protected Robot_RecipeData recipieData;
    [SerializeField]
    private int creditProfit = 0;
    
    public override void HandleInteract()
    {
        base.HandleInteract();

        if(transform.parent == WorkshopBench.Instance.tabManager.robotParent)
        {
            EventBus.Publish(EventType.ROBOT_TAKEN_OFF_WORKBENCH);
        }
    }

    public bool CheckContract(Player_Contract_Manager player_Contract_Manager)
    {
        for (int i = 0; i < player_Contract_Manager.Contract_DataList.Count; i++)
        {
            if (player_Contract_Manager.Contract_DataList[i].Contract_Status == ContractStatus.InProgress &&
                player_Contract_Manager.Contract_DataList[i].Robot_RecipeData == recipieData)
            {
                SellRobot(player_Contract_Manager.Contract_DataList[i]);
                return true;
            }
        }
        return false;
    }

    public void SellRobot(Contract_Data contract)
    {
        EventBus.Publish(EventType.ROBOT_SOLD, recipieData);

        WorkshopManager.Instance.WorkshopStorage.CreditCount += creditProfit;
    }
}
