using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RogueBotConfig : ScriptableObject
{
    // Currently used for settting the path during chase state and checking if the player is within the various unit spheres for patrolling/chasing/charging
    [Tooltip("How often various RogueBot actions will be checked. Measured in seconds.")]
    public float tickRate;

    [Tooltip("The Layer the player is tagged under.")]
    public LayerMask playerLayerMask;

    #region Patrol
    [Header("Patrol Stats")]
    [Tooltip("Speed of the RogueBot while patrolling.")]
    public float patrolSpeed;

    [Tooltip("Acceleration of the RogueBot while patrolling.")]
    public float patrolAcceleration;

    [Tooltip("Angular Speed of the RogueBot while patrolling.")]
    public float patrolAngularSpeed;

    [Tooltip("Range of the RogueBot's patrol radius.")] 
    public float patrolRange;

    [Tooltip("Center point of the RogueBot's patrol radius.")] 
    public Vector3 patrolCenterPoint;

    [Tooltip("Minimum distance between patrol points.")] 
    public float minDistFromLastPoint;

    [Tooltip("Minimum time the RogueBot will wait at a patrol point.")] 
    public float minWaitTime;

    [Tooltip("Maximum time the RogueBot will wait at a patrol point.")] 
    public float maxWaitTime;
    #endregion

    #region Chase
    [Header("Chase Stats")]
    [Tooltip("Speed of the RogueBot during chase state.")]
    public float chaseSpeed;

    [Tooltip("Acceleration of the RogueBot during chase state.")]
    public float chaseAcceleration;

    [Tooltip("Angular Speed of the RogueBot while chasing.")]
    public float chaseAngularSpeed;

    [Tooltip("Range that the RogueBot will detect the player and begin to chase them. Indicated by a blue wire sphere.")]
    public float chaseRange;

    [Tooltip("Length of time (in seconds) the detected sprite will exist for.")]
    public float spriteFlashTime;
    #endregion

    #region Charge
    [Header("Charge Stats")]
    [Tooltip("Speed of the RogueBot during charge state.")]
    public float chargeSpeed;

    [Tooltip("Acceleration of the RogueBot during charge state.")]
    public float chargeAcceleration;

    [Tooltip("Speed of the RogueBot while turning during charge state.")]
    public float chargeAngularSpeed;

    [Tooltip("Range that the RogueBot will charge the player. Indicated by a red wire sphere.")]
    public float chargeRange;

    [Tooltip("The time (in seconds) that the RogueBot will pause in place before charging.")]
    public float pauseBeforeChargeTime;

    [Tooltip("The time (in seconds) that the RogueBot will be charging for.")]
    public float chargeDuration;
    #endregion
}
