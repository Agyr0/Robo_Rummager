using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerPatrolState : ScavengerState
{
    public ScavengerStateId GetId()
    {
        return ScavengerStateId.Patrol;
    }
    
    public void Enter(ScavengerAgent agent)
    {
    }

    public void Update(ScavengerAgent agent)
    {
    }

    public void Exit(ScavengerAgent agent)
    {
    }
}
