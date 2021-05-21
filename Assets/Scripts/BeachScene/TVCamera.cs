using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class TVCamera : MonoBehaviour
{
    [SerializeField] private Transform[] _smokeSpawnPositions;

    private BoxCollider _collider;
    private Rigidbody _rigidbody;

    public BoxCollider Collider => _collider;
    public Rigidbody Rigidbody => _rigidbody;
    public Transform[] SmokeSpawnPositions => _smokeSpawnPositions;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }
}
