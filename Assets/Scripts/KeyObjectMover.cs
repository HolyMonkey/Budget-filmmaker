using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class KeyObjectMover : MonoBehaviour
{
    [SerializeField] private KeyObjectGhost _objectGhost;
    [SerializeField] private float _minDistanceToTarget;
    [SerializeField] private ActionsDemonstrator _actionDemonstrator;
    [SerializeField] private HandPointer _handPointer;
    [SerializeField] protected float DraggingSpeed;

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
        _handPointer.MouseDown += OnPointerDown;
    }

    private void OnDisable()
    {
        _actionDemonstrator.ActionStarted -= OnAcitonStarted;
        _handPointer.MouseDown -= OnPointerDown;
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
    protected abstract void OnPointerDown(Vector2 mousePosition);

    protected void IsCloseToTarget()
    {
        if (Vector3.Distance(_objectGhost.transform.position, transform.position) <= _minDistanceToTarget)
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
