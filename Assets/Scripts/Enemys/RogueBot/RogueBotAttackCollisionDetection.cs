using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBotAttackCollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.playerController.Health -= 10;
            Debug.Log(GameManager.Instance.playerController.Health);
        }
    }
}
