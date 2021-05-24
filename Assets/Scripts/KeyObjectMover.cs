using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class KeyObjectMover : MonoBehaviour
{
    [SerializeField] private ActionsDemonstrator _actionsDemonstrator;
    [SerializeField] private HandPointer _handPointer;
    [SerializeField] protected KeyObjectGhost ObjectGhost;
    [SerializeField] protected float MinDistanceToTarget;
    [SerializeField] protected float DraggingSpeed;

    private bool _isActionStarted;

    protected Vector3 StartPosition;
    protected bool IsTargetReached;
    protected bool IsDragging;

    public event UnityAction TargetReached;
    public event UnityAction DraggingStarted;
    public event UnityAction DraggingEnded;

    private void OnEnable()
    {
        _actionsDemonstrator.ActionStarted += OnAcitonStarted;
        _handPointer.MouseDown += OnPointerDown;
    }

    private void OnDisable()
    {
        _actionsDemonstrator.ActionStarted -= OnAcitonStarted;
        _handPointer.MouseDown -= OnPointerDown;
    }

    private void Start()
    {
        StartPosition = transform.position;
    }

    private void Update()
    {
        if (IsTargetReached == false && _isActionStarted == false)
        {
            TryMove();
        }
    }

    protected abstract void TryMove();
    protected abstract void OnPointerDown(Vector2 mousePosition);

    protected virtual void IsCloseToTarget()
    {
        if (Vector3.Distance(ObjectGhost.transform.position, transform.position) <= MinDistanceToTarget)
        {
            IsTargetReached = true;
            TargetReached?.Invoke();
        }
        else
        {
            transform.position = StartPosition;
        }
    }

    protected void ReachTarget()
    {
        TargetReached?.Invoke();
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
