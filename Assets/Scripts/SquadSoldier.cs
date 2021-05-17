using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class SquadSoldier : MonoBehaviour
{
    [SerializeField] private string _animatorSpeedParameter;
    [SerializeField] private Transform _destinationPoint;
    [SerializeField] private float _minDistanceFromDestination;

    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isReachedDestination;

    public NavMeshAgent Agent => _agent;
    public Transform DestinationPoint => _destinationPoint;
    public event UnityAction DestinationReached;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(_animatorSpeedParameter, _agent.velocity.magnitude);

        if (_isReachedDestination == false)
        {
            if (Vector3.Distance(transform.position, _destinationPoint.position) <= _minDistanceFromDestination)
            {
                Debug.Log("Destination reached");
                _isReachedDestination = true;
                DestinationReached?.Invoke();
            }
        }
    }
}
