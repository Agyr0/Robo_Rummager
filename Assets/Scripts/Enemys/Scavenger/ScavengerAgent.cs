using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScavengerAgent : MonoBehaviour, IDamageable
{
    public ObjectPooler objectPooler;
    public ScavengerWeaponIK scavengerWeaponIK;
    public ScavengerStateMachine stateMachine;
    public ScavengerStateId initialState;
    public NavMeshAgent navMeshAgent;
    public ScavengerConfig config;
    public ScavengerSensor scavengerSensor;
    public Animator animator;

    public float scavengerMaxHealth = 100;
    public float scavengerHealth;

    private void Awake()
    {
        objectPooler = GameObject.Find("Object Pool Manager").GetComponent<ObjectPooler>();
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        scavengerSensor = GetComponent<ScavengerSensor>();
        animator = GetComponent<Animator>();
        config = ScavengerConfig.Instantiate(config);
        stateMachine = new ScavengerStateMachine(this);
        stateMachine.RegisterState(new ScavengerPatrolState());
        stateMachine.RegisterState(new ScavengerDetectionState());
        stateMachine.RegisterState(new ScavengerShootingState());
        stateMachine.RegisterState(new ScavengerRepositionState());
        stateMachine.ChangeState(initialState);
        scavengerHealth = scavengerMaxHealth;

        // Temp Code to setup pathing for scavenger patrol
        // TODO: Create a spawn manager for scavengers that will properly assign a scavengerPatrolPath
        config.scavengerPatrolPath = GameObject.Find("Scavenger Patrol Path");

        foreach(Transform child in config.scavengerPatrolPath.transform)
        {
            config.scavengerPatrolPoints.Add(child);
        }
    }

    private void Update()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        stateMachine.Update();
    }

    public void TakeDamage(float damage)
    {
        scavengerHealth -= damage;
        if (scavengerHealth <= 0)
        {
            Debug.Log("Scavenger Shot");
            LootBag lootBag = this.gameObject.GetComponent<LootBag>();
            lootBag.DropResource(this.gameObject.transform.position);
            gameObject.SetActive(false);
            navMeshAgent.enabled = false;
            scavengerHealth = scavengerMaxHealth;
        }
    }
}
