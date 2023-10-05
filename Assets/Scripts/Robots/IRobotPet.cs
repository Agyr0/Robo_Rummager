using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRobotPet
{
    public bool CheckContract(Player_Contract_Manager player_Contract_Manager);

    public void SellRobot(Contract_Data contract);
}