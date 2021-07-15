using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class FindFood : State<Ant>
//{    
//    public override void Enter(Ant subject)
//    {
//        subject.wander.enabled = true;
        
//    }
//    public override void Excute(Ant subject)
//    {
//        subject.LeaveColonyPheromone();

//        if (subject.isFindFood)
//        {
//            subject.stateMachine.ChangeState(new FindColony(),subject);
//            subject.wander.enabled = false;
//        }
//    }
//    public override void Exit(Ant subject)
//    {
//        subject.wander.enabled = false;
//    }
//}

//public class FindColony : State<Ant>
//{
//    public override void Enter(Ant subject)
//    {        
//        subject.wander.enabled = true;
//    }
//    public override void Excute(Ant subject)
//    {
//        subject.LeaveFoodPheromone();

//        if (subject.isFindColony)
//            subject.stateMachine.ChangeState(new FindFood(), subject);
//    }
//    public override void Exit(Ant subject)
//    {
//        subject.wander.enabled = false;
//    }
//}