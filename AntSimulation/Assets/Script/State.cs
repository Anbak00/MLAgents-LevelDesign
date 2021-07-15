using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class State<T>
{
    public abstract void Enter(T subject);
    public abstract void Excute(T subject);
    public abstract void Exit(T subject);
}
