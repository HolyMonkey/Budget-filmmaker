using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [SerializeField] private KeyObjectGhost _objectGhost;
    [SerializeField] private KeyObjectMover _mover;
    [SerializeField] private float _positionAdjustmentSpeed;

    protected bool IsInCorrectPlace;

    public bool IsObjectInCorrectPlace => IsInCorrectPlace;
    public KeyObjectGhost ObjectGhost => _objectGhost;
    public float PositionAdjustmentSpeed => _positionAdjustmentSpeed;

    private void OnEnable()
    {
        _mover.TargetReached += OnTargetReached;
    }

    private void OnDisable()
    {
        _mover.TargetReached -= OnTargetReached;
    }

    private void OnTargetReached()
    {
        StartCoroutine(SuccessMovementRoutine());
    }

    protected virtual IEnumerator SuccessMovementRoutine()
    {
        while (transform.position != _objectGhost.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _objectGhost.transform.position, _positionAdjustmentSpeed * Time.deltaTime);
            yield return null;
        }

        IsInCorrectPlace = true;
        _objectGhost.Disappear();
    }
}
