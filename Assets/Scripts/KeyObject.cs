using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [SerializeField] private KeyObjectMover _mover;

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
        Debug.Log("Target is reached");
    }
}
