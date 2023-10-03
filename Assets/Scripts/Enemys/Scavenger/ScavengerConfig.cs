using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScavengerConfig : ScriptableObject
{
    // Currently used for settting the path during chase state and checking if the player is within the various unit spheres for patrolling/chasing/charging
    [Tooltip("How often various Scavenger actions will be checked. Measured in seconds.")]
    public float tickRate;

    #region Patrol
    [Header("Patrol Stats")]
    [Tooltip("Speed of the Scavenger while patrolling.")]
    public float patrolSpeed;

    [Tooltip("Acceleration of the Scavenger while patrolling.")]
    public float patrolAcceleration;

    [Tooltip("Angular Speed of the Scavenger while patrolling.")]
    public float patrolAngularSpeed;

    [Tooltip("Minimum time the Scavenger will wait at a patrol point.")]
    public float minWaitTime;

    [Tooltip("Maximum time the Scavenger will wait at a patrol point.")]
    public float maxWaitTime;
    #endregion
}
