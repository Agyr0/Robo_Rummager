using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public float duration;

    [HideInInspector]
    public Cinemachine.CinemachineImpulseSource cameraShake;


    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    public void GenerateRecoil()
    {
        cameraShake.GenerateImpulse(Camera.main.transform.forward);
    }
}
