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
        int numStates = System.Enum.GetNames(typeof(RogueBotStateId)).Length + 1; // Only reading up to 3 states for some reason???
        states = new ScavengerState[numStates];                                   // adding additional numStates arbitrarily is my fix B)
    }

    public void RegisterState(ScavengerState state)
    {
        int index = (int)state.GetId();
        Debug.Log("index: " + index);
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
