using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBotStateMachine
{
    public RogueBotState[] states;
    public RogueBotAgent agent;
    public RogueBotStateId currentState;

    public RogueBotStateMachine(RogueBotAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(RogueBotStateId)).Length;
        states = new RogueBotState[numStates];
    }

    public void RegisterState(RogueBotState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public RogueBotState GetState(RogueBotStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(RogueBotStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
