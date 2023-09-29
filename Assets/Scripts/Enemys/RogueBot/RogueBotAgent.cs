using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class RogueBotAgent : MonoBehaviour, IDamageable
{
    public RogueBotStateMachine stateMachine;
    public RogueBotStateId initialState;
    public NavMeshAgent navMeshAgent;
    public RogueBotConfig config;

    public GameObject detectedIcon;
    public GameObject chargeHitbox;
    private float rogueBotMaxHealth = 75;
    public float rogueBotHealth;
    public Material rogueBotMat;
    public float blinkIntensity;
    public float blinkDuration;
    private float blinkTimer;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new RogueBotStateMachine(this);
        stateMachine.RegisterState(new RogueBotPatrolState());
        stateMachine.RegisterState(new RogueBotChaseState());
        stateMachine.RegisterState(new RogueBotChargeState());
        stateMachine.ChangeState(initialState);
        config.patrolCenterPoint = this.transform.position;
        rogueBotHealth = rogueBotMaxHealth;
    }

    void Update()
    {
        stateMachine.Update();

        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = lerp * blinkIntensity;
        rogueBotMat.color = Color.white * intensity;
    }

    public void TakeDamage(float damage)
    {
        blinkTimer = blinkDuration;
        rogueBotHealth -= damage;
        if (rogueBotHealth <= 0)
        {
            LootBag lootBag = this.gameObject.GetComponent<LootBag>();
            lootBag.DropResource(this.gameObject.transform.position);
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the Patrolling Range
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireSphere(config.patrolCenterPoint, config.patrolRange);

        // Draw the Chase Range
        Gizmos.color = UnityEngine.Color.blue;
        Gizmos.DrawWireSphere(transform.position, config.chaseRange);

        // Draw the Charge Range
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, config.chargeRange);
    }
}
