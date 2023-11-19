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

    public Animator animator;


    public override void HandleInteract()
    {

        if(transform.parent == WorkshopBench.Instance.tabManager.robotParent)
        {
            EventBus.Publish(EventType.ROBOT_TAKEN_OFF_WORKBENCH);
        }
        base.HandleInteract();
    }

    public bool CheckContract(Player_Contract_Manager player_Contract_Manager)
    {
        if (player_Contract_Manager.Contract_Data.Contract_Status == ContractStatus.InProgress &&
            player_Contract_Manager.Contract_Data.Robot_RecipeData == recipieData)
        {
            SellRobot(player_Contract_Manager.Contract_Data);
            return true;
        }
        return false;
    }

    public void SellRobot(Contract_Data contract)
    {
        EventBus.Publish(EventType.ROBOT_SOLD, recipieData);

        WorkshopManager.Instance.WorkshopStorage.CreditCount += creditProfit;
    }

}
