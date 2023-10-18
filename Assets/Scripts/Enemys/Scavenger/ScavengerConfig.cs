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

    [Tooltip("The gameobject that contains the points for the scavengers patrolling path.")]
    public GameObject scavengerPatrolPath;

    [Tooltip("The points that the Scavenger will patrol around, should be grabbed from the children of the ScavengerSpawnPoint.")]
    public List<Transform> scavengerPatrolPoints;

    [Tooltip("Should the agent patrol randomly(true) or should they go in order of the points (false).")]
    public bool randomPatrol;
    #endregion

    #region Detection
    [Header("Detection Stats")]
    [Tooltip("Speed of the Scavenger while in detection state.")]
    public float detectionSpeed;

    [Tooltip("Acceleration of the Scavenger while in detection state.")]
    public float detectionAcceleration;

    [Tooltip("Angular Speed of the Scavenger while in detecction state.")]
    public float detectionAngularSpeed;

    [Tooltip("The time in seconds that the scavenger will chase the player before giving up and returing to the patrol state.")]
    public float detectionStateMaxTime;
    #endregion

    #region Shooting
    [Header("Shooting Stats")]
    [Tooltip("Speed of the Scavenger while in shooting state.")]
    public float shootingSpeed;

    [Tooltip("Acceleration of the Scavenger while in shooting state.")]
    public float shootingAcceleration;

    [Tooltip("Angular Speed of the Scavenger while in shooting state.")]
    public float shootingAngularSpeed;

    [Tooltip("The minimum number of shots the scavenger will shoot.")]
    public int minShots;

    [Tooltip("The maximum number of shots the scavenger will shoot.")]
    public int maxShots;

    [Tooltip("The time between scavenger shots.")]
    public float timeBetweenShots;

    [Tooltip("The speed of the scavenger bullets.")]
    public float bulletSpeed;
    #endregion

    #region Reposition
    [Header("Shooting Stats")]
    [Tooltip("Speed of the Scavenger while in shooting state.")]
    public float repositionSpeed;

    [Tooltip("Acceleration of the Scavenger while in shooting state.")]
    public float repositionAcceleration;

    [Tooltip("Angular Speed of the Scavenger while in shooting state.")]
    public float repositionAngularSpeed;

    [Tooltip("Minimum distance from the player the AI can be when repositioning")]
    public float minDistanceFromPlayer;

    [Tooltip("Maximum distance from the player the AI can be when repositioning")]
    public float maxDistanceFromPlayer;
    #endregion
}
