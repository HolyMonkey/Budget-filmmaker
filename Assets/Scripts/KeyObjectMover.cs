using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class KeyObjectMover : MonoBehaviour
{
    [SerializeField] protected Transform TargetPoint;
    [SerializeField] private float _minDistanceToTarget;

    protected bool IsDragging;
    protected bool IsTargetReached;

    public event UnityAction TargetReached;

    private void Update()
    {
        if (IsTargetReached == false)
        {
            TryMove();
        }
    }

    protected abstract void TryMove();

    protected void IsCloseToTarget()
    {
        if (Mathf.Abs((TargetPoint.position).magnitude - (transform.position).magnitude) <= _minDistanceToTarget)
        {
            IsTargetReached = true;
            TargetReached?.Invoke();
        }
    }
}
