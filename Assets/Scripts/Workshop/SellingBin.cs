using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBin : MonoBehaviour
{
    private Player_Contract_Manager player_Contract_Manager;

    private void Start()
    {
        player_Contract_Manager = Player_Contract_Manager.Instance;
    }


    private void OnTriggerEnter(Collider other)
    {
        BaseRobotPetController controller = other.GetComponent<BaseRobotPetController>();
        if (controller != null)
        {
            controller.CheckContract(player_Contract_Manager);
        }
    }


}
