using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    public GameObject tutorialManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            tutorialManager.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
