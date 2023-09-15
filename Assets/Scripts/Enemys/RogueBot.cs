using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RogueBot : MonoBehaviour, IDamageable
{
    private NavMeshAgent agent;
    private Transform playerTransform;
    private float maxHealth = 75;
    private float health;

    [SerializeField]
    private GameObject detectedSprite;

    [SerializeField]
    private GameObject rogueBotAttackHitbox;

    [SerializeField]
    private Animator rogueBotTestAttackAnimator;

    [SerializeField]
    private LayerMask playerLayerMask;

    #region Patrolling
    private float patrolWaitTime;
    [Header("Patrol Settings")]

    [SerializeField]
    [Tooltip("Speed of the RogueBot during patrol state.")]
    private float patrolSpeed;

    [SerializeField]
    [Tooltip("Acceleration of the RogueBot during patrol state.")]
    private float patrolAcceleration;

    [SerializeField]
    [Tooltip("Range that the Rogue Bot will patrol around in. Indicated by a green wire sphere.")]
    private float patrolRange;

    [SerializeField]
    [Tooltip("Center Point that the RogueBot will patrol around.")]
    private Transform patrolCenterPoint;

    [SerializeField]
    [Tooltip("Minimum distance that can be between new patrol points.")]
    private float minDistFromLastPoint;

    [SerializeField]
    [Tooltip("Minimum time the RogueBot will wait at a location before moving to another.")]
    private float minWaitTime;

    [SerializeField]
    [Tooltip("Maximum time the RogueBot will wait at a location before moving to another.")]
    private float maxWaitTime;
    #endregion

    #region Chasing
    private bool robotInLeashRange;
    private bool playerInChaseRange;
    private bool hasPlayedDetectionSprite;
    [Header("Chase Settings")]

    [SerializeField]
    [Tooltip("Speed of the RogueBot during chase state.")]
    private float chaseSpeed;

    [SerializeField]
    [Tooltip("Acceleration of the RogueBot during chase state.")]
    private float chaseAcceleration;

    [SerializeField]
    [Tooltip("Range that the RogueBot will detect the player and begin to chase them. Indicated by a blue wire sphere.")]
    private float chaseRange;

    [SerializeField]
    [Tooltip("Range that the RogueBot will chase the player within. Indicated by a yellow wire sphere.")]
    private float leashRange;

    [SerializeField]
    [Tooltip("Length of time (in seconds) the detected sprite will exist for.")]
    private float spriteFlashTime;
    #endregion

    #region Charging
    private bool playerInChargeRange;
    private bool canCharge = true;
    [Header("Charge Settings")]

    [SerializeField]
    [Tooltip("Speed of the RogueBot during charge state.")]
    private float chargeSpeed;

    [SerializeField]
    [Tooltip("Acceleration of the RogueBot during charge state.")]
    private float chargeAcceleration;

    [SerializeField]
    [Tooltip("Range that the RogueBot will charge the player. Indicated by a red wire sphere.")]
    private float chargeRange;

    [SerializeField]
    [Tooltip("Range that the RogueBot will stop before the player when charging.")]
    private float chargeStopRange;

    [SerializeField]
    [Tooltip("The time (in seconds) that the RogueBot will wait before charging the player again.")]
    private float timeBetweenCharge;

    [SerializeField]
    [Tooltip("The time (in seconds) that the RogueBot will pause in place after charging.")]
    private float chargeEndLagTime;
    #endregion

    void Start()
    {
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        // Check if player is in charge range
        playerInChargeRange = Physics.CheckSphere(transform.position, chargeRange, playerLayerMask);

        // Check if player is in chase range
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, playerLayerMask);

        // Check if robot is in the leash range
        if (Vector3.Distance(transform.position, patrolCenterPoint.transform.position) > leashRange)
            robotInLeashRange = false;
        else
            robotInLeashRange = true;



        // State Handler
        if (playerInChargeRange && robotInLeashRange)
            RogueBotCharging();
        else if (playerInChaseRange && robotInLeashRange)
            RogueBotChasing();
        else
            RogueBotPatrolling();
    }

    // Patrolling
    private void RogueBotPatrolling()
    {
        hasPlayedDetectionSprite = false;
        agent.speed = patrolSpeed;
        agent.acceleration = patrolAcceleration;

        if (agent.remainingDistance <= agent.stoppingDistance) // Check if Rogue Bot has reached position
        {
            patrolWaitTime -= Time.deltaTime; // Countdown the wait timer
            if (patrolWaitTime <= 0)
            {
                Vector3 point;
                if (RandomPoint(patrolCenterPoint.position, patrolRange, out point))
                {
                    agent.SetDestination(point); // If timer hits 0 and within stopping distance set a new point
                }
            }
        }
        else
        {
            if (patrolWaitTime <= 0)
                patrolWaitTime = Random.Range(minWaitTime, maxWaitTime); // If timer hits 0 and not within stopping distance reset timer for next point
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; // Generate a random point
            float distFromLastPoint = Vector3.Distance(this.transform.position, randomPoint);

            if (distFromLastPoint < minDistFromLastPoint)
                randomPoint = center + Random.insideUnitSphere * range; // Generate a random point if the first point was within the minimum distance

            else if (distFromLastPoint > minDistFromLastPoint)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))  // Find closest point on NavMesh
                {
                    result = hit.position;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }
    }

    // Chasing
    private void RogueBotChasing()
    {
        if (hasPlayedDetectionSprite == false)
        {
            StartCoroutine(FlashDetectedSprite());
        }
        agent.speed = chaseSpeed;
        agent.acceleration = chaseAcceleration;
        agent.SetDestination(playerTransform.position);
    }

    // Charging
    private void RogueBotCharging()
    {
        agent.speed = chargeSpeed;
        agent.acceleration = chargeAcceleration;
        if (canCharge == true)
        {
            if (agent.remainingDistance <= chargeStopRange)
            {
                agent.ResetPath();
                StartCoroutine(ResetChargeAttackCooldown());
                StartCoroutine(RogueBotAttacking());
                canCharge = false;
            }
        }
        else if (canCharge == false)
        {
            StartCoroutine(ChargeEndLag());
        }
    }

    // Attacking Animation
    IEnumerator RogueBotAttacking()
    {
        rogueBotTestAttackAnimator.Play("RogueBotTestAttack");
        yield return null;
        StartCoroutine(ActivateHitbox());
    }

    IEnumerator FlashDetectedSprite()
    {
        detectedSprite.SetActive(true);
        yield return new WaitForSeconds(spriteFlashTime);
        detectedSprite.SetActive(false);
        hasPlayedDetectionSprite = true;
    }

    IEnumerator ResetChargeAttackCooldown()
    {
        yield return new WaitForSeconds(timeBetweenCharge);
        canCharge = true;
    }

    IEnumerator ChargeEndLag()
    {
        agent.velocity = Vector3.zero;
        yield return new WaitForSeconds(chargeEndLagTime);
        RogueBotChasing();
    }

    IEnumerator ActivateHitbox()
    {
        rogueBotAttackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        rogueBotAttackHitbox.SetActive(false);
    }

    public void TakeDamage()
    {
        health -= 25;
        Debug.Log("Damage Taken, health at:" + health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the Patrolling Range
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireSphere(patrolCenterPoint.position, patrolRange);

        // Draw the Chase Range
        Gizmos.color = UnityEngine.Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Draw the Charge Range
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, chargeRange);

        // Draw the Leash Range
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireSphere(patrolCenterPoint.position, leashRange);
    }
}
