using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BeachMan : MonoBehaviour
{
    [SerializeField] private KeyObjectGhost _ghost;

    private Vector3 _startPosition;
    private Animator _animator;

    public KeyObjectGhost Ghost => _ghost;
    public Vector3 StartPosition => _startPosition;
    public Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _startPosition = transform.position;
    }
}
