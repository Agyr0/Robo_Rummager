using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerStateMachine
{
    public ScavengerState[] states;
    public ScavengerAgent agent;
    public ScavengerStateId currentState;

    public ScavengerStateMachine(ScavengerAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(RogueBotStateId)).Length;
        states = new ScavengerState[numStates];
    }

    public void RegisterState(ScavengerState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public ScavengerState GetState(ScavengerStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(ScavengerStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
