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

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        scavengerSensor = this.gameObject.GetComponent<ScavengerSensor>();
        config = ScavengerConfig.Instantiate(config);
        stateMachine = new ScavengerStateMachine(this);
        stateMachine.RegisterState(new ScavengerPatrolState());
        stateMachine.RegisterState(new ScavengerDetectionState());
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
