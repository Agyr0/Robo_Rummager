using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class RogueBotAgent : MonoBehaviour, IDamageable
{
    public AudioManager audioManager;
    public AudioSource audioSource;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public RogueBotStateMachine stateMachine;
    public RogueBotStateId initialState;
    public RogueBotConfig config;
    
    public GameObject detectedIcon;
    public GameObject chargeHitbox;

    public float rogueBotMaxHealth = 75;
    public float rogueBotHealth;

    void Start()
    {
        audioManager = AudioManager.Instance;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // State Machine Stuff
        stateMachine = new RogueBotStateMachine(this);
        stateMachine.RegisterState(new RogueBotPatrolState());
        stateMachine.RegisterState(new RogueBotChaseState());
        stateMachine.RegisterState(new RogueBotChargeState());
        stateMachine.RegisterState(new RogueBotRepositionState());
        config = RogueBotConfig.Instantiate(config);

        // Stuff to do when enemy is spawned
        stateMachine.ChangeState(initialState);
        rogueBotHealth = rogueBotMaxHealth;
        config.patrolCenterPoint = transform.position;
    }

    void Update()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        stateMachine.Update();
    }

    public void TakeDamage(float damage)
    {
        rogueBotHealth -= damage;
        if (rogueBotHealth <= 0)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("Dead", true);
            // Play Death Audio
            audioManager.PlayClip(audioSource, audioManager.FindRandomizedClip(AudioType.RogueBot_Death, audioManager.effectAudio));
            StartCoroutine(DespawnRogueBot());
        }
        else
        {
            // Chase Player
            stateMachine.ChangeState(RogueBotStateId.Chase);
        }
    }

    IEnumerator DespawnRogueBot()
    {
        yield return new WaitForSeconds(1.069f);
        // Item Drops
        LootBag lootBag = this.gameObject.GetComponent<LootBag>();
        lootBag.DropResource(this.gameObject.transform.position);

        // Respawn and Object Pool stuff
        navMeshAgent.isStopped = false;
        gameObject.SetActive(false);
        navMeshAgent.enabled = false;
        rogueBotHealth = rogueBotMaxHealth;
        animator.SetBool("Dead", false);
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
