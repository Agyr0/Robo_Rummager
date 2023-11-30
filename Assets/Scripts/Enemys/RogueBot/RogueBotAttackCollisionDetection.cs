using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBotAttackCollisionDetection : MonoBehaviour
{
    public RogueBotAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.playerController.TakeDamage(agent.config.damage);
            Debug.Log(GameManager.Instance.playerController.Health);
        }
    }
}
