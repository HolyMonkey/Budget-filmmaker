using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class SquadSoldier : MonoBehaviour
{
    [SerializeField] private string _animatorSpeedParameter;
    [SerializeField] private Transform _destinationPoint;

    private NavMeshAgent _agent;
    private Animator _animator;

    public NavMeshAgent Agent => _agent;
    public Transform DestinationPoint => _destinationPoint;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(_animatorSpeedParameter, _agent.velocity.magnitude);
    }
}
