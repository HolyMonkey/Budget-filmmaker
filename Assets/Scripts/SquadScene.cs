using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquadScene : ActionScene
{
    [SerializeField] private SquadSoldier[] _soldiers;

    private int _soldiersReachedDestination;

    private void OnEnable()
    {
        foreach (SquadSoldier soldier in _soldiers)
        {
            soldier.DestinationReached += OnDestinationReached;
        }
    }

    private void OnDisable()
    {
        foreach (SquadSoldier soldier in _soldiers)
        {
            soldier.DestinationReached -= OnDestinationReached;
        }
    }

    public override void Run()
    {
        Debug.Log("Start squad scene");
        foreach (SquadSoldier soldier in _soldiers)
        {
            soldier.Agent.SetDestination(soldier.DestinationPoint.position);
        }
    }

    private void OnDestinationReached()
    {
        _soldiersReachedDestination++;
        if (_soldiersReachedDestination == _soldiers.Length)
        {
            CompleteScene();
        }
    }
}
