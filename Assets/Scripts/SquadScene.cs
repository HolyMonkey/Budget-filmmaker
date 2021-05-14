using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquadScene : ActionScene
{
    [SerializeField] private SquadSoldier[] _soldiers;

    public override void Run()
    {
        Debug.Log("Start squad scene");
        foreach (SquadSoldier soldier in _soldiers)
        {
            soldier.Agent.SetDestination(soldier.DestinationPoint.position);
        }
    }
}
