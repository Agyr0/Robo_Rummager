using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGfxController : MonoBehaviour
{
    public void ShowWeapon()
    {
        EventBus.Publish(EventType.DISPLAY_WEAPON);
    }
    public void RaycastWrench()
    {
        EventBus.Publish(EventType.WRENCH_RAYCAST);
    }
}