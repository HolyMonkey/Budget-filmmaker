using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class KeyObjectMover : MonoBehaviour
{
    [SerializeField] private KeyObjectGhost _objectGhost;
    [SerializeField] private float _minDistanceToTarget;

    private bool _isTargetReached;
    
    protected bool IsDragging;

    public event UnityAction TargetReached;

    private void Update()
    {
        if (_isTargetReached == false)
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
    }
}
