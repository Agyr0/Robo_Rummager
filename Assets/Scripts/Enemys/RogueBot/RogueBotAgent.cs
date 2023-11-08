using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class RogueBotAgent : MonoBehaviour, IDamageable
{
    public AudioManager audioManager;
    public AudioSource audioSource;
    public RogueBotStateMachine stateMachine;
    public RogueBotStateId initialState;
    public NavMeshAgent navMeshAgent;
    public RogueBotConfig config;

    public GameObject detectedIcon;
    public GameObject chargeHitbox;
    private float rogueBotMaxHealth = 75;
    public float rogueBotHealth;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioManager = AudioManager.Instance;
        audioSource = GetComponent<AudioSource>();
        config = RogueBotConfig.Instantiate(config);
        stateMachine = new RogueBotStateMachine(this);
        stateMachine.RegisterState(new RogueBotPatrolState());
        stateMachine.RegisterState(new RogueBotChaseState());
        stateMachine.RegisterState(new RogueBotChargeState());
        stateMachine.ChangeState(initialState);
        rogueBotHealth = rogueBotMaxHealth;
    }

    void Update()
    {
        stateMachine.Update();
    }

    public void TakeDamage(float damage)
    {
        rogueBotHealth -= damage;
        if (rogueBotHealth <= 0)
        {
            // Chase Player
            stateMachine.ChangeState(RogueBotStateId.Chase);

            // Item Drops
            LootBag lootBag = this.gameObject.GetComponent<LootBag>();
            lootBag.DropResource(this.gameObject.transform.position);
            
            // Respawn and Object Pool stuff
            gameObject.SetActive(false);
            navMeshAgent.enabled = false;
            rogueBotHealth = rogueBotMaxHealth;
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
