using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    State<T> curState;    
    
    public StateMachine(State<T> state, T subject)
    {

        curState = state;
        curState.Enter(subject);
    }

    public void Enter(T subject)
    {
        curState.Enter(subject);
    }

    public void Excute(T subject)
    {
        curState.Excute(subject);
    }

    public void Exit(T subject)
    {
        curState.Exit(subject);
    }

    public void ChangeState(State<T> state, T subject)
    {
        curState.Exit(subject);
        curState = state;
        curState.Enter(subject);
    }
}
