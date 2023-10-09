using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScavengerAgent : MonoBehaviour, IDamageable
{
    public ScavengerStateMachine stateMachine;
    public ScavengerStateId initialState;
    public NavMeshAgent navMeshAgent;
    public ScavengerConfig config;
    public ScavengerSensor scavengerSensor;
    private Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        scavengerSensor = this.gameObject.GetComponent<ScavengerSensor>();
        config = ScavengerConfig.Instantiate(config);
        stateMachine = new ScavengerStateMachine(this);
        stateMachine.RegisterState(new ScavengerPatrolState());
        stateMachine.RegisterState(new ScavengerDetectionState());
        stateMachine.ChangeState(initialState);

        animator = GetComponent<Animator>();

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
        // Take damage yo!
    }
}
