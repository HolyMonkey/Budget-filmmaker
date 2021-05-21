using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorAnimationEventHandler : MonoBehaviour
{
    [SerializeField] private Operator _operator;
    [SerializeField] private Animator _operatorAnimator;
    [SerializeField] private TVCamera _camera;
    [SerializeField] private float _throwForce;

    public void ThrowCamera()
    {
        _camera.transform.SetParent(_operator.transform);
        _camera.Collider.enabled = true;
        _camera.Rigidbody.isKinematic = false;
        Vector3 force = new Vector3(1, 1, 0).normalized * _throwForce;
        _camera.Rigidbody.AddForce(force);
        _operatorAnimator.SetLayerWeight(1, 0);
    }
}
