using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier1BaseController : BaseRobotPetController, IInteractable, IRobotPet
{

    public void DestoryRobot()
    {
        Destroy(gameObject);
    }



}
