using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScavengerStateId
{
    Patrol,
    Approach,
    Shoot,
}

public interface ScavengerState
{
    ScavengerStateId GetId();
    void Enter(ScavengerAgent agent);
    void Update(ScavengerAgent agent);
    void Exit(ScavengerAgent agent);
}
