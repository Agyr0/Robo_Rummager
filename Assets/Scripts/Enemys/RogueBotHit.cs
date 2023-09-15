using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBotHit : MonoBehaviour
{
    public interface IDamageable
    {
        Vector3 Position { get; }
        void Damage(float damage);
    }
}
