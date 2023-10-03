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

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        config = ScavengerConfig.Instantiate(config);
        stateMachine = new ScavengerStateMachine(this);
        stateMachine.RegisterState(new ScavengerPatrolState());
        stateMachine.ChangeState(initialState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void TakeDamage(float damage)
    {
        // Take damage yo!
    }
}
