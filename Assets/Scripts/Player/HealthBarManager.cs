using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        playerController = GameManager.Instance.playerController;
    }

    public void SetMaxHealth(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetHealth(float value)
    {
        slider.value = value;
    }
}
