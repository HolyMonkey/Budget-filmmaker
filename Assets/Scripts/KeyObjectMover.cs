using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class KeyObjectMover : MonoBehaviour
{
    [SerializeField] private KeyObjectGhost _objectGhost;
    [SerializeField] private float _minDistanceToTarget;
    [SerializeField] private ActionsDemonstrator _actionDemonstrator;

    private bool _isTargetReached;
    private bool _isActionStarted;
    private Vector3 _startPosition;
    
    protected bool IsDragging;

    public event UnityAction TargetReached;
    public event UnityAction DraggingStarted;
    public event UnityAction DraggingEnded;

    private void OnEnable()
    {
        _actionDemonstrator.ActionStarted += OnAcitonStarted;
    }

    private void OnDisable()
    {
        _actionDemonstrator.ActionStarted -= OnAcitonStarted;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_isTargetReached == false && _isActionStarted == false)
        {
            TryMove();
        }
    }

    protected abstract void TryMove();

    protected void IsCloseToTarget()
    {
        if (Mathf.Abs(_objectGhost.transform.position.magnitude - transform.position.magnitude) <= _minDistanceToTarget)
        {
            _isTargetReached = true;
            TargetReached?.Invoke();
        }
        else
        {
            transform.position = _startPosition;
        }
    }

    protected void StartDragging()
    {
        DraggingStarted?.Invoke();
    }

    protected void EndDragging()
    {
        DraggingEnded?.Invoke();
    }

    private void OnAcitonStarted()
    {
        _isActionStarted = true;
    }
}
