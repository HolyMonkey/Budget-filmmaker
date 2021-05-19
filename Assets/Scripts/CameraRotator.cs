using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private Transform _cameraRig;
    [SerializeField] private KeyObjectMover[] _movers;
    [SerializeField] private float _resetTime;
    [SerializeField] private ActionsDemonstrator _actionDemonstrator;
    //[SerializeField] private Transform _handPointerRig;

    private Vector3 _mousePreviousPosition;
    private bool _isRotationAllowed = true;
    private Quaternion _startRotation;
    //private Quaternion _handPointerStartRotation;

    public event UnityAction CameraReset;

    private void OnEnable()
    {
        foreach (KeyObjectMover mover in _movers)
        {
            mover.DraggingStarted += OnDraggingStarted;
            mover.DraggingEnded += OnDraggingEnded;
        }

        _actionDemonstrator.ActionStarted += OnActionStarted;
    }

    private void OnDisable()
    {
        foreach (KeyObjectMover mover in _movers)
        {
            mover.DraggingStarted -= OnDraggingStarted;
            mover.DraggingEnded -= OnDraggingEnded;
        }

        _actionDemonstrator.ActionStarted -= OnActionStarted;
    }

    private void Start()
    {
        _startRotation = _cameraRig.rotation;
        //_handPointerStartRotation = _handPointerRig.rotation;
    }

    private void Update()
    {
        if (_isRotationAllowed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mousePreviousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 passedDistance = Camera.main.ScreenToViewportPoint(Input.mousePosition) - _mousePreviousPosition;
                _cameraRig.Rotate(new Vector3(0, 1, 0), -passedDistance.x * 180 * Time.deltaTime * _sensitivity);
                //_handPointerRig.Rotate(new Vector3(0, 1, 0), -passedDistance.x * 180 * Time.deltaTime * _sensitivity);
                _mousePreviousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
        }
    }

    private void OnDraggingStarted()
    {
        _isRotationAllowed = false;
    }

    private void OnDraggingEnded()
    {
        _isRotationAllowed = true;
    }

    public void ResetRotation()
    {
        StartCoroutine(WaitForEndOfReset());
    }

    private IEnumerator WaitForEndOfReset()
    {
        Quaternion currentRotation = _cameraRig.rotation;
        //Quaternion currentHandPointerRotation = _handPointerRig.rotation;
        float passedTime = 0;

        if (_cameraRig.rotation != _startRotation)
        {
            while (passedTime < _resetTime)
            {
                _cameraRig.rotation = Quaternion.Lerp(currentRotation, _startRotation, passedTime / _resetTime);
                //_handPointerRig.rotation = Quaternion.Lerp(currentHandPointerRotation, _handPointerStartRotation, passedTime / _resetTime);

                passedTime += Time.deltaTime;
                yield return null;
            }
        }

        _cameraRig.rotation = _startRotation;
        //_handPointerRig.rotation = _handPointerStartRotation;
        CameraReset?.Invoke();
    }

    private void OnActionStarted()
    {
        _isRotationAllowed = false;
    }
}
