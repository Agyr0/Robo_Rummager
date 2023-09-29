using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RogueBotConfig : ScriptableObject
{
    // Currently used for settting the path during chase state and checking if the player is within the various unit spheres for patrolling/chasing/charging
    [Tooltip("Number of ticks (in seconds) that the Rogue Bot updates on")]
    public float tickRate;

    [Tooltip("The Layer the player is tagged under.")]
    public LayerMask playerLayerMask;

    #region Patrol
    [Header("Patrol Stats")]
    [Tooltip("Speed of the RogueBot while patrolling.")]
    public float patrolSpeed;

    [Tooltip("Acceleration of the RogueBot while patrolling.")]
    public float patrolAcceleration;

    [Tooltip("Angular Acceleration of the RogueBot while patrolling.")]
    public float patrolAngularAcceleration;

    [Tooltip("Range of the RogueBot's patrol radius.")] 
    public float patrolRange;

    [Tooltip("Center point of the RogueBot's patrol radius.")] 
    public Vector3 patrolCenterPoint;

    [Tooltip("Minium distance between patrol points.")] 
    public float minDistFromLastPoint;

    [Tooltip("Minium wait time at a patrol point.")] 
    public float minWaitTime;

    [Tooltip("Maximum wait time at a patrol point.")] 
    public float maxWaitTime;
    #endregion

    #region Chase
    [Header("Chase Stats")]
    [Tooltip("Speed of the RogueBot during chase state.")]
    public float chaseSpeed;

    [Tooltip("Acceleration of the RogueBot during chase state.")]
    public float chaseAcceleration;

    [Tooltip("Angular Acceleration of the RogueBot while chasing.")]
    public float chaseAngularAcceleration;

    [Tooltip("Range that the RogueBot will detect the player and begin to chase them. Indicated by a blue wire sphere.")]
    public float chaseRange;

    [Tooltip("Length of time (in seconds) the detected sprite will exist for.")]
    public float spriteFlashTime;
    #endregion

    #region Charge
    [Header("Charge Stats")]
    [Tooltip("Range that the RogueBot will charge the player. Indicated by a red wire sphere.")]
    public float chargeRange;

    [Tooltip("The time (in seconds) that the RogueBot will pause in place before charging.")]
    public float pauseBeforeChargeTime;

    [Tooltip("The time (in seconds) that the RogueBot will be chargin for.")]
    public float chargeDuration;
    #endregion
}
