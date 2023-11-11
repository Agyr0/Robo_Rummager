using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RogueBotStateId
{
    Patrol,
    Chase,
    Charge,
    Reposition
}
public interface RogueBotState
{
    RogueBotStateId GetId();
    void Enter(RogueBotAgent agent);
    void Update(RogueBotAgent agent);
    void Exit(RogueBotAgent agent);
}
